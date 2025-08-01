
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MusterıBasvuru.Models.ViewModel;
using Microsoft.Extensions.Caching.Memory;
using MusterıBasvuru.Service;
using MusterıBasvuru.Entity.Model;
using MusterıBasvuru.DbContext;
using Bogus.Extensions.Extras;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json; // .NET 5+ için

namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly IMailService _mailService;
        private readonly User _logger;
        private readonly LogService _logService;

        private readonly UygulamaDbContext _context;
        private object _userManager;

        public TokenController( IConfiguration configuration, IMemoryCache cache,IMailService mailService,UygulamaDbContext context)
        {
            _configuration = configuration;
            _cache = cache;
            _mailService= mailService;
            _context = context; 
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
                return BadRequest("Geçersiz kullanıcı adı veya parola.");

            HttpContext.Session.SetString("MusteriNo", user.MusteriNo);

            return Ok(new { message = "Giriş başarılı", role = user.Role });
        }


        private string GenerateRandomPassword(int length = 6)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(password);
        }
        [HttpPost("kayitolustur")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Ad = model.Ad,
                Soyad = model.Soyad,
                TCKimlikNO = model.TCKimlikNO,
                MusteriNo = GenerateRandomPassword(),
                Email = model.Email,
                DogumTarihi = model.DogumTarihi,
                Role =  "User",

                Password= GenerateRandomPassword(),
            };

         
             _context.User.Add(user);
            await _mailService.RandomSıfre(user.Password, user.Email);
            await _context.SaveChangesAsync();


            return Ok(new { Message = "User created successfully." });
        }

      
    }

}
