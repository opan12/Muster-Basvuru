namespace MusterıBasvuru.Service
{
    public interface IMailService
    {
        Task onaymail(string email);
        Task redmail(string email, string mesaj);
        Task RandomSıfre(string sıfre, string toEmail);
    }
}
