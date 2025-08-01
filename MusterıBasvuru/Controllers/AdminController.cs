using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusterıBasvuru.DbContext;
using MusterıBasvuru.Entity;
using MusterıBasvuru.Entity.Dto;
using MusterıBasvuru.Entity.Enum;
using MusterıBasvuru.Entity.Model;
using MusterıBasvuru.Models.ViewModel;
using MusterıBasvuru.Service;
using System.Diagnostics.Metrics;
//using SimpleLogger;
namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UygulamaDbContext _context;
        string userName = Environment.UserName;
        private readonly LogService _logservice;
        private readonly IMailService _mailService;

        public AdminController(UygulamaDbContext context, LogService logservice, IMailService mailService)
        {
            _context = context;
            _logservice = logservice;
            _mailService = mailService;
        }
        [HttpPost("adminlogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            var adminUser = await _context.AdminUser
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (adminUser == null)
                return BadRequest("Geçersiz kullanıcı adı veya parola.");

            // Session'a rol ve kullanıcı adı kaydet
            HttpContext.Session.SetString("Username", adminUser.Username);
            HttpContext.Session.SetString("Role", "Admin");

            return Ok(new { message = "Giriş başarılı", role = adminUser.Role });
        }

        public class CreateAdminModel
        {
            public string? Username { get; set; }
            public string? Password { get; set; }  
            public string? Role { get; set; }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Kullanıcı adı ve parola gerekli.");

            var existingAdmin = await _context.AdminUser
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (existingAdmin != null)
                return Conflict("Bu kullanıcı adı zaten kayıtlı.");

            var adminUser = new AdminUser
            {
                Username = model.Username,
                Password = model.Password,  // Düz metin şifre burada
                Role = "Admin",
            };

            _context.AdminUser.Add(adminUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Admin kullanıcı oluşturuldu." });
        }
    



        [HttpGet("admin/data")]
        public IActionResult GetAdminData()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return Unauthorized("Bu alana erişim yetkiniz yok.");

            // Admin işlemleri
            return Ok("Admin özel verisi");
        }


        [HttpGet("musteriliste")]
        public async Task<IActionResult> Get()
        {
            var musteriBasvuruDto = _context.MusteriBasvuru.Select(t => new MusteriBasvuruDto
                    {
                        MusteriNo = t.MusteriNo,
                        BasvuruDurum = t.BasvuruDurum,
                        Basvurutipi = t.Basvurutipi,
                        BasvuruTarihi = t.BasvuruTarihi,
                        HataAciklama = t.HataAciklama,
                        Kayit_Zaman = t.Kayit_Zaman,
                        Kayit_Yapan = t.Kayit_Yapan,
                        Kayit_Durum = t.Kayit_Durum
                    });
            

            return Ok(musteriBasvuruDto);
        }
       

        [HttpPost("onay")]
        public async Task<IActionResult> Onay(MusteriBasvuruDto basvuru)
        {
            var username= Environment.UserName;

            var musteribasvuru = new MusteriBasvuru
            {
                MusteriBasvuru_UID = Guid.NewGuid(),
                MusteriNo = basvuru.MusteriNo,
                BasvuruDurum = Durum.Onaylandi,
                Basvurutipi = basvuru.Basvurutipi.Value,
                BasvuruTarihi = DateTime.Now,
                HataAciklama = basvuru.HataAciklama,
                Kayit_Zaman = DateTime.Now,
                Kayit_Yapan = username ,
                Kayit_Durum = "Aktif"
            };
            _context.MusteriBasvuru.Add(musteribasvuru);


            await _context.SaveChangesAsync();

            return Ok(basvuru);
        }


        [HttpPut("{id}")]
        public async Task <IActionResult> Put(Guid id, [FromBody] MusteriBasvuruDto edit)
        {
            var musteribasvuru = _context.MusteriBasvuru.FirstOrDefault(t => t.MusteriBasvuru_UID == id);
         
            {
                edit.BasvuruDurum = Durum.Reddedildi;
                edit.Kayit_Durum = "Pasif";
                edit.HataAciklama = musteribasvuru.HataAciklama;
                    };
           _context.MusteriBasvuru.Update(musteribasvuru);
     

        await _context.SaveChangesAsync();
            return Ok(musteribasvuru);
        }
    }
}
