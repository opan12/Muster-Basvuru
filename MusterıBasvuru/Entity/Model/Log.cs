using MusterıBasvuru.Entity.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MusterıBasvuru.Entity
{
    [Table("Log")]

    public class Log
    {
        [Key]
        public Guid logId { get; set; }
        public Guid MusteriBasvuru_UID { get; set; }
        public Guid Basvuru_UID { get; set; }

        public string Açıklama { get; set; }

        public string Kayit_Yapan { get; set; }
        public DateTime Kayit_Zaman { get; set; }
    }
}
