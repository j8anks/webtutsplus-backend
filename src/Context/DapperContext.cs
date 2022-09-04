using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DapperASPNetCore.Context
{
	public class DapperContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("MySqlDb");
		}

		public IDbConnection CreateConnection()
			=> new MySqlConnection(_connectionString);
		
			
	}
}
