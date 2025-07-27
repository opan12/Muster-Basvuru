using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using MusterıBasvuru.Entity.Model;
using MusterıBasvuru.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MusterıBasvuru.Helper
{
    public class GenerateToken
    {

        
            private readonly UserManager<User> _userManager;
            private readonly IConfiguration _configuration;
            private readonly IMemoryCache _cache;
            private readonly MailService _mailService;


            public GenerateToken(UserManager<User> userManager, IConfiguration configuration, IMemoryCache cache, MailService mailService)
            {
                _userManager = userManager;
                _configuration = configuration;
                _cache = cache;
                _mailService = mailService;
            }



          
    }
}
