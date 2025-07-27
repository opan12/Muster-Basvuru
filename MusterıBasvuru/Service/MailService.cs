using System.Net.Mail;
using System.Net;
using MusterıBasvuru.Entity.Enum;
using MusterıBasvuru.Entity;

namespace MusterıBasvuru.Service
{
    public class MailService:IMailService
    {
        public async Task onaymail(string toEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sudeopann@gmail.com", "fajv jphv oxac xzmm"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sudeopann@gmail.com"),
                Subject = "Banka başvurusu",
                Body = $" onaylanmıştır",
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw;
            }

        }

        public async Task MusteriGonder(string toEmail, int durum, string açıklama)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sudeopann@gmail.com", "fajv jphv oxac xzmm"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sudeopann@gmail.com"),
                Subject = "Banka başvurusu",
                Body = $"{durum} reddedilmiştir ",
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw;
            }
        }
        public async Task RandomSıfre(string sıfre,string toEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sudeopann@gmail.com", "fajv jphv oxac xzmm"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sudeopann@gmail.com"),
                Subject = "Banka başvurusu",
                Body = $"{sıfre} ",
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail); // ← toEmail burada null olabilir


            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw;
            }
        }

        public Task redmail(string email, string mesaj)
        {
            throw new NotImplementedException();
        }
    }
}


