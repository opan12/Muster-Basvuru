using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusterıBasvuru.Entity.Model;

[Table("User")]
public class User
{
    [Key]
    public Guid MusteriBasvuru_UID { get; set; }

    public string MusteriNo { get; set; }
    public string Username { get; set; }

    public string TCKimlikNO { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public DateTime DogumTarihi { get; set; }


}