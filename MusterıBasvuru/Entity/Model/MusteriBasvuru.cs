using MusterıBasvuru.Entity.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusterıBasvuru.Entity.Model
{
    [Table("MusteriBasvuru")]

    public class MusteriBasvuru
    {
        [Key]
        public Guid Basvuru_UID { get; set; }
        public Guid MusteriBasvuru_UID { get; set; }

        public string MusteriNo { get; set; }

        public Durum BasvuruDurum { get; set; }

        public Tip Basvurutipi { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public string HataAciklama { get; set; }
        public DateTime Kayit_Zaman { get; set; }
        public string Kayit_Yapan { get; set; }
        public string Kayit_Durum { get; set; }

    }
}
