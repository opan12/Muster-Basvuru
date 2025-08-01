namespace MusterıBasvuru.Entity.Model
{
    public class AdminUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }

    }
}
