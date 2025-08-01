using MusterıBasvuru.Entity.Enum;

namespace MusterıBasvuru.Entity.Dto
{
    public class MusteriBasvuruDto
    {
        public string? MusteriNo { get; set; }              // ? ile nullable
        public Durum? BasvuruDurum { get; set; }            // enum nullable
        public Tip? Basvurutipi { get; set; }                // enum nullable
        public DateTime? BasvuruTarihi { get; set; }         // nullable DateTime
        public string? HataAciklama { get; set; }
        public DateTime? Kayit_Zaman { get; set; }
        public string? Kayit_Yapan { get; set; }
        public string? Kayit_Durum { get; set; }
    }
}
