using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusterıBasvuru.DbContext;
using MusterıBasvuru.Entity;
using MusterıBasvuru.Entity.Dto;
using MusterıBasvuru.Entity.Enum;
using MusterıBasvuru.Entity.Model;
using MusterıBasvuru.Service;
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

        public AdminController(UygulamaDbContext context, LogService logservice,IMailService mailService)
        {
            _context = context;
            _logservice = logservice;
            _mailService = mailService;
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
                Basvurutipi = basvuru.Basvurutipi,
                BasvuruTarihi = DateTime.Now,
                HataAciklama = basvuru.HataAciklama,
                Kayit_Zaman = DateTime.Now,
                Kayit_Yapan = username ,
                Kayit_Durum = "Aktif"
            };
            _context.MusteriBasvuru.Add(musteribasvuru);

            ////var logger = new Logger();
            ////logger.LogBasvuruAlindi();
            await _context.SaveChangesAsync();
            await _logservice.AddLogAsync();

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
     
           await _logservice.AddLogAsync();

        await _context.SaveChangesAsync();
            return Ok(musteribasvuru);
        }
    }
}
