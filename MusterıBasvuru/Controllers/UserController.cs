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

namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UygulamaDbContext _context;
        public UserController(UygulamaDbContext context)
        {
            _context = context;
        }
        [HttpGet("kısısellıste")]
        public async Task<IActionResult> MusterGet(string musterino, MusteriBasvuru musteriBasvuruDto)
        {

            var musteriBasvuru = _context.MusteriBasvuru.Where(t => t.MusteriNo == musterino && t.Kayit_Durum == "Aktif");
            {
                if (!musteriBasvuru.Any())
                {
                    return NotFound("Müşteri bulunamadı.");
                }


                var musteriBasvuruDtoList = musteriBasvuru.Select(t => new MusteriBasvuruDto
                {
                    MusteriNo = t.MusteriNo,
                    BasvuruDurum = t.BasvuruDurum,
                    Basvurutipi = t.Basvurutipi,
                    BasvuruTarihi = t.BasvuruTarihi,
                    HataAciklama = t.HataAciklama,

                }).ToList();

                return Ok(musteriBasvuruDtoList);
            }




        }
        [HttpPost("musteriform")]
        public async Task<IActionResult> musteriform([FromBody] MusteriBasvuruDto musteriBasvuruDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 🔍 Hata detaylarını döner
            }

            var basvuru = new MusteriBasvuru
            {
                MusteriBasvuru_UID = Guid.NewGuid(),
                MusteriNo = musteriBasvuruDto.MusteriNo,
                BasvuruDurum = Durum.Beklemede,
                Basvurutipi = musteriBasvuruDto.Basvurutipi,
                BasvuruTarihi = DateTime.Now,
                HataAciklama = "fghjök",
                Kayit_Zaman = DateTime.Now,
                Kayit_Yapan = "fjklş",
                Kayit_Durum = "Aktif"
            };

            _context.MusteriBasvuru.Add(basvuru);
            await _context.SaveChangesAsync();

            return Ok(basvuru);
        }
    }
}