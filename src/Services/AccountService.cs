using WebApplication.Models;
using DapperASPNetCore.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.JwtHelpers;
using System.Web.Helpers;
using DapperASPNetCore.Dto;
using System.Security.Claims;

// https://brockallen.com/2012/10/19/password-management-made-easy-in-asp-net-with-the-crypto-api/

namespace DapperASPNetCore.Services   
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _acctRepo;
        private readonly JwtSettings jwtSettings;
        public AccountService(JwtSettings jwtSettings, IAccountRepository acctRepo)
        {
            this.jwtSettings = jwtSettings;
            _acctRepo = acctRepo;
        }

        public async Task<UserTokens> GetUserFromDb(User userLogin)
        {

            var user = await _acctRepo.GetUserFromDb(userLogin);

            try
            {
                var Token = new UserTokens();

                if (user == null)
                {
                    return Token;
                }

                var valid = CheckPassword(user.Password, userLogin.Password);

                if (valid)
                {
                    
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EntityId = user.EntityId,
                        EmailId = user.Email,
                        GuidId = Guid.NewGuid(),
                        UserName = user.Username,
                        UserId = user.UserId,
                        firstname = user.FirstName,
                        lastname = user.LastName,
                        role =  user.Role,
                        Id = Guid.NewGuid(),

                    }, jwtSettings);
                }
                else
                {
                    return Token;
                }
                return Token;
            }
            catch (Exception)
            {
                throw;
            }
        }
                

        public bool CheckPassword(string savedHashedPassword, string unhashedPassword)
        {  
            return Crypto.VerifyHashedPassword(savedHashedPassword, unhashedPassword);
        }

        public async Task<AccountDto> CreateAccount(AccountDto account)
        {
            account.UserUID = JwtHelpers.IdFactory(11);
            account.Password = Crypto.HashPassword(account.Password);
            account.SaltKey = JwtHelpers.IdFactory(20);            
            account.RegistrationDateUtc = DateTime.UtcNow;

            var newAccount = await _acctRepo.CreateAccount(account);

            return newAccount;
        }

        public IdentityDto UserClaims(ClaimsIdentity? identity)
        {
            IdentityDto userClaims = new IdentityDto();

            if (identity != null)
            {
                // IEnumerable<Claim> claims = identity.Claims;
                // or
                userClaims.UserId = identity.FindFirst("UserId").Value;
                userClaims.EntityId = identity.FindFirst("EntityId").Value;

            }
            else
            {
                userClaims.EntityId = "0";
            }

            return userClaims;
        }

        

        //var query = "SELECT category_name, description, image_url FROM categories WHERE Id = @Id";

        //using (var connection = _context.CreateConnection())
        //{
        //    var category = await connection.QuerySingleOrDefaultAsync<CategoryDto>(query, new { id });

        //    return category;
        //}
    }


        //public Task<TodoItem[]> GetIncompleteItemsAsync()
        //{
        //    var item1 = new TodoItem
        //    {
        //        Title = "Learn ASP.NET Core",
        //        DueAt = DateTimeOffset.Now.AddDays(1)
        //    };

        //    var item2 = new TodoItem
        //    {
        //        Title = "Build awesome apps",
        //        DueAt = DateTimeOffset.Now.AddDays(2)
        //    };

        //    return Task.FromResult(new[] { item1, item2 });
        //}
   // }
}
