using MusterıBasvuru.DbContext;
using MusterıBasvuru.Entity;
using MusterıBasvuru.Entity.Model;

namespace MusterıBasvuru.Service
{
    public class LogService: ILogService
    {
        private readonly UygulamaDbContext _context;

        public LogService(UygulamaDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync()
        {
            var  kayityapan= Environment.UserName;
            var log = new Log
            {
                Kayit_Yapan = kayityapan,
                Kayit_Zaman = DateTime.Now
            };

            await _context.SaveChangesAsync();
        }
    }
}
