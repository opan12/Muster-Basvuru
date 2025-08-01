using MusterıBasvuru.DbContext;
using MusterıBasvuru.Entity;
using MusterıBasvuru.Entity.Model;

namespace MusterıBasvuru.Service
{
    public class LogService
    {
        private readonly UygulamaDbContext _context;

        public LogService(UygulamaDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(Guid userıd, Guid basvuruıd)
        {
            var kayityapan = Environment.UserName;
            var log = new Log
            {
                Basvuru_UID = basvuruıd,
                Kayit_Zaman = DateTime.Now,
                Kayit_Yapan = kayityapan,
                Açıklama = string.Empty,
                MusteriBasvuru_UID = userıd,
            };
            _context.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
