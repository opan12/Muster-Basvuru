using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusterıBasvuru.Entity.Model
{
    [Table("Musteriİletisim")]

    public class Musteriİletisim
    {
        [Key]
        public int IletısımId { get; set; }
         public Guid MusteriBasvuru_UID { get; set; }
        public Guid Basvuru_UID { get; set; }
        public string Acıklama { get; set; }

    }
}
