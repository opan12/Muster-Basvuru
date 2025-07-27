namespace MusterıBasvuru.Entity.Model
{
    public class BasvuruBildirimLog
    {
        public int Id { get; set; }
        public Guid BasvuruId { get; set; }
        public string MusteriNo { get; set; }
        public string YeniDurum { get; set; }
        public string Aciklama { get; set; }
        public DateTime BildirimTarihi { get; set; }
        public bool IsProcessed { get; set; }
    }

}
