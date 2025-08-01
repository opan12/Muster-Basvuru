using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusterıBasvuru.DbContext;
using MusterıBasvuru.Entity;

namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly UygulamaDbContext _context;
        public LogController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet("logkayit")]
        public async Task<IActionResult> Log()
            
        {
            var Log = _context.Log.Select(t => new Log
            {
                Kayit_Zaman = t.Kayit_Zaman,
                Kayit_Yapan = t.Kayit_Yapan,
                Açıklama = t.Açıklama,
                logId = t.logId,
                MusteriBasvuru_UID = t.MusteriBasvuru_UID
            });

            return Ok(Log);
        }
    }
}
