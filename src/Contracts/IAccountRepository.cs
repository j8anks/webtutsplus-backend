using WebApplication.Models;
using DapperASPNetCore.Dto;
namespace DapperASPNetCore.Contracts
{

	public interface IAccountRepository
	{
		public Task<User> GetUser(User userLogin);
		public Task<UserLoginDto> GetUserFromDb(User userLogin);
		public Task<bool> UsernameExists(string userName);
		public Task<bool> UserEmailExists(string userEmail);
		public Task<bool> UserNameOrEmailExists(string userName, string userEmail);
		public Task<AccountDto> CreateAccount(AccountDto account);
	}
}
