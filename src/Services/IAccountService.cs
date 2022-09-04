using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;
using DapperASPNetCore.Dto;
using System.Security.Claims;

namespace DapperASPNetCore.Services
{
    public interface IAccountService
    {
              


        //Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<UserTokens> GetUserFromDb(User userLogin);
        Task<AccountDto> CreateAccount(AccountDto account);
        IdentityDto UserClaims(ClaimsIdentity? identity);

        //Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
    }
}