using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private readonly DapperContext _context;

		public AccountRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<User> GetUser(User userLogin)
		{
			var query = "SELECT username, PasswordHash FROM users WHERE username = @username or email = @username";

			var parameters = new DynamicParameters();
			parameters.Add("username", userLogin.Email, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var user = await connection.QuerySingleOrDefaultAsync<User>(query, parameters);

				return user;
			}
		}

		public async Task<UserLoginDto> GetUserFromDb(User userLogin)
		{
			var query = "SELECT UserID, EntityId, username, FirstName, LastName, email, PasswordHash As Password, Role FROM users WHERE username = @username or email = @username";

			var parameters = new DynamicParameters();
			parameters.Add("username", userLogin.Email, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var user = await connection.QuerySingleOrDefaultAsync<UserLoginDto>(query, parameters);

				return user;
			}
		}

		public async Task<bool> UsernameExists(string userName)
        {
			var query = @"with data as
                    (
                        select 1 as 'Username'
                    )
                    select CASE WHEN EXISTS (SELECT Username FROM users WHERE Username = @Username)
                           THEN 1 
                           ELSE 0
                      END AS result 
                    from data ";

			var parameters = new DynamicParameters();
			parameters.Add("username", userName, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var userExists = await connection.ExecuteScalarAsync<bool>(query, parameters);
								
				return userExists;
			}
		}

		public async Task<bool> UserEmailExists(string userEmail)
		{
			var query = @"with data as
                    (
                        select 1 as 'Email'
                    )
                    select CASE WHEN EXISTS (SELECT Email FROM users WHERE Email = @Email)
                           THEN 1 
                           ELSE 0
                      END AS result 
                    from data ";

			var parameters = new DynamicParameters();
			parameters.Add("email", userEmail, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var userEmailExists = await connection.ExecuteScalarAsync<bool>(query, parameters);

				return userEmailExists;
			}
		}

		public async Task<bool> UserNameOrEmailExists(string userName, string userEmail)
		{
			var query = @"with data as
                    (
                        select 1 as 'Email'
                    )
                    select CASE WHEN EXISTS (SELECT Email, UserName FROM users WHERE Username = @Username Or Email = @Email)
                           THEN 1 
                           ELSE 0
                      END AS result 
                    from data ";

			var parameters = new DynamicParameters();
			parameters.Add("username", userName, DbType.String);
			parameters.Add("email", userEmail, DbType.String);
			

			using (var connection = _context.CreateConnection())
			{
				var userEmailExists = await connection.ExecuteScalarAsync<bool>(query, parameters);

				return userEmailExists;
			}
		}

		public async Task<AccountDto> CreateAccount(AccountDto account)
		{
			var query = @"
			Insert into users
			  (Username, UserGUID, Email, PasswordHash, SaltKey, RegistrationDate) VALUES (@Username, @UserGUID, @Email, @PasswordHash, @SaltKey, @RegistrationDate);

			select LAST_INSERT_ID();";
					
			var parameters = new DynamicParameters();
			parameters.Add("Username", account.Username, DbType.String);
			parameters.Add("UserGUID", account.UserUID, DbType.String);
			parameters.Add("Email", account.Email, DbType.String);
			parameters.Add("PasswordHash", account.Password, DbType.String);
			parameters.Add("SaltKey", account.SaltKey, DbType.String);
			parameters.Add("RegistrationDate", account.RegistrationDateUtc, DbType.DateTime);


			using (var connection = _context.CreateConnection())
			{
				var id = await connection.ExecuteScalarAsync<int>(query, parameters);

				var createdAccount = new AccountDto
				{					
					Username = account.Username,
					Email = account.Email					
				};

				return createdAccount;
			}
		}

	}
}