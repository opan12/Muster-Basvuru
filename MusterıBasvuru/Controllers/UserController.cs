using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusterıBasvuru.Entity.Enum;
using MusterıBasvuru.Entity.Dto;
using MusterıBasvuru.Service;
using MusterıBasvuru.Entity.Model;
using MusterıBasvuru.DbContext;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using MusterıBasvuru.Entity;


namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LogService _logService;

        private readonly UygulamaDbContext _context;
        public UserController(UygulamaDbContext context,LogService logService)
        {
            _context = context;
            _logService= logService;
        }
        [HttpGet("kisiselliste")]
        public async Task<IActionResult> KisiListe()
        {
            var musteriNo = HttpContext.Session.GetString("MusteriNo");

            if (string.IsNullOrEmpty(musteriNo))
                return Unauthorized("Session’da MusteriNo yok.");

            var liste = await _context.MusteriBasvuru
                .Where(m => m.MusteriNo == musteriNo)
                .ToListAsync();

            return Ok(liste);
        }



        [HttpPost("musteriform")]
        public async Task<IActionResult> musteriform([FromBody] MusteriBasvuruDto musteriBasvuruDto)
        {
            try
            {
                if (!musteriBasvuruDto.Basvurutipi.HasValue)
                    return BadRequest(new { error = "Başvuru tipi boş olamaz." });

                var musteriNo = HttpContext.Session.GetString("MusteriNo");
                if (string.IsNullOrEmpty(musteriNo))
                    return Unauthorized("Session’da MusteriNo yok.");

                var user = await _context.User.FirstOrDefaultAsync(u => u.MusteriNo.ToString() == musteriNo);

                if (user == null)
                    return NotFound(new { error = "Kullanıcı bulunamadı." });

                var username = Environment.UserName;

                var basvuru = new MusteriBasvuru
                {
                    MusteriBasvuru_UID = Guid.NewGuid(),
                    MusteriNo = user.MusteriNo,
                    BasvuruDurum = Durum.Beklemede,
                    Basvurutipi = musteriBasvuruDto.Basvurutipi.Value,
                    BasvuruTarihi = DateTime.Now,
                    HataAciklama = "",
                    Kayit_Zaman = DateTime.Now,
                    Kayit_Yapan = username,
                    Kayit_Durum = "Aktif"
                };
                _context.MusteriBasvuru.Add(basvuru);
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync(user.MusteriBasvuru_UID, basvuru.Basvuru_UID);



            }


            catch (Exception ex)
{
    var log = new ErrorLog
    {
        Message = ex.Message,
        StackTrace = ex.StackTrace,
        Path = HttpContext.Request.Path,
        UserName = Environment.UserName
    };

        _context.ErrorLogs.Add(log);
    await _context.SaveChangesAsync();

    return StatusCode(500, "Sunucu hatası");
    }
            return Ok(new { mesaj = "Başvuru başarıyla kaydedildi." });
        }

    }
}