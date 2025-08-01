using Microsoft.EntityFrameworkCore;
using MusterıBasvuru.Entity;
using MusterıBasvuru.Entity.Model;

namespace MusterıBasvuru.DbContext
{
    public class UygulamaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<MusteriBasvuru> MusteriBasvuru { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Musteriİletisim> MusteriIletisim{ get; set; }
    }
}
