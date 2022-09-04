using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Services;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Cors;

namespace WebApplication.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/user")] // [controller]/[action]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private readonly IAccountService _accountService;
        public AccountController(JwtSettings jwtSettings, IAccountService accountService)
        {
            this.jwtSettings = jwtSettings;
            _accountService = accountService;
        }


        private IEnumerable<Users> logins = new List<Users>()
        {
            new Users()
            {
                Id = Guid.NewGuid(),
                EmailId = "adminakp@gmail.com",
                UserName ="Admin",
                Password="Admin",
            },
            new Users()
            {
                Id = Guid.NewGuid(),
                EmailId = "adminakp@gmail.com",
                UserName ="User1",
                Password="Admin",
            }
        };

        /// <summary>
        /// Generate an Access token
        /// </summary>
        /// <param name="userLogins"></param>
        /// <returns></returns>
        [HttpPost ("signin")]
        
        public async Task<IActionResult> GetToken(User userLogin)
        {
            
             var token = await _accountService.GetUserFromDb(userLogin);

            if (token.Token == null)
            {
                return BadRequest($"Wrong username or password");
                
            }

            return Ok(token);

        }

        
        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(AccountDto account)
        {
            try
            {
                var createdAccount = await _accountService.CreateAccount(account);
                
                return Ok("Account Created");
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        //public IActionResult GetToken(UserLogins userLogins)
        //{

        //    userLogins.UserName = "jbanks";

        //  var test =   _accountService.GetUserFromDb(userLogins);

        //    try
        //    {
        //        var Token = new UserTokens();
        //        var Valid = logins.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
        //        if (Valid)
        //        {
        //            var user = logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
        //            Token = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens()
        //            {
        //                EmailId = user.EmailId,
        //                GuidId = Guid.NewGuid(),
        //                UserName = user.UserName,
        //                Id = user.Id,

        //            }, jwtSettings);
        //        }
        //        else
        //        {
        //            return BadRequest($"wrong password");
        //        }
        //        return Ok(Token);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        /// <summary>
        /// Get List of UserAccounts   
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            return Ok(logins);
        }
    }
}


