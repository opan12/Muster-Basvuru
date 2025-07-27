
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

namespace MusterıBasvuru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly MailService _mailService;


        public TokenController(UserManager<User> userManager, IConfiguration configuration, IMemoryCache cache,MailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _cache = cache;
            _mailService = mailService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null /*|| !(await _userManager.CheckPasswordAsync(user, model.Password)*/)
            {
                return BadRequest("Invalid username or password.");
            }

            return Ok();
        }

        private string GenerateRandomPassword(int length = 12)
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
                Password= GenerateRandomPassword(),
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _mailService.RandomSıfre( user.Password,user.Email);
            return Ok(new { Message = "User created successfully." });
        }

      
    }

}
