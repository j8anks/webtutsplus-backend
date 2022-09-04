﻿using Dapper;
using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly DapperContext _context;

		public CompanyRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Company>> GetCompanies()
		{
			// SELECT Id, tstamp As Name, pressure As Address, temperature as Country

			var query = @"SELECT * FROM del_companies   ";

			// var parameters = new DynamicParameters();
			//parameters.Add("Page", pager.Offset, DbType.Int32);
			//parameters.Add("PageSize", pager.PageSize, DbType.Int32);

			// GetCompanies1();

			using (var connection = _context.CreateConnection())
			{
				var companies = await connection.QueryAsync<Company>(query);
				return companies.ToList();
			}
		}

		public async void GetCompanies1()
		{
			var query = @"select Count(id) from xcert_chart_data; 
			SELECT Id, tstamp As Name, pressure As Address, temperature as Country FROM xcert_chart_data 
            ORDER BY id LIMIT @Page, @PageSize;  ";

			var parameters = new DynamicParameters();
			parameters.Add("Page", 1);
			parameters.Add("PageSize", 10, DbType.Int32);

			using (var connection = _context.CreateConnection().QueryMultiple(query, parameters))
            {
                var companyCount = connection.Read<int>().Single();
				var customer = connection.Read<Company>().ToList();

				// return (Company)companies;
			}

			using (var con = _context.CreateConnection())
            {
				using (var multi = con.QueryMultiple(query, parameters))
				{
					var companyCount = multi.Read<int>().Single();
					var customer = multi.Read<Company>().ToList();
				}
			}

			

			//using (var connection = _context.CreateConnection())
			//{
			//	var companyCount = connection.QueryMultiple(query, parameters).Read<int>().Single();
			//	//var customer = connection.QueryMultiple(query, parameters).Read<Company>().ToList();

			//	// return (Company)companies;
			//}



		}



		public async Task<Company> GetCompany(int id)
		{
			var query = "SELECT * FROM del_companies WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

				return company;
			}
		}

		public async Task<Company> CreateCompany(CompanyForCreationDto company)
		{			
			var query = @"
			Insert into del_companies
			  (Name, Address, Country) VALUES (@Name, @Address, @Country);

			select LAST_INSERT_ID();";

			var parameters = new DynamicParameters();
			parameters.Add("Name", company.Name, DbType.String);
			parameters.Add("Address", company.Address, DbType.String);
			parameters.Add("Country", company.Country, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				var id = await connection.ExecuteScalarAsync<int>(query, parameters);

				var createdCompany = new Company
				{
					Id = id,
					Name = company.Name,
					Address = company.Address,
					Country = company.Country
				};

				return createdCompany;
			}
		}

		public async Task UpdateCompany(int id, CompanyForUpdateDto company)
		{
			var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);
			parameters.Add("Name", company.Name, DbType.String);
			parameters.Add("Address", company.Address, DbType.String);
			parameters.Add("Country", company.Country, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task DeleteCompany(int id)
		{
			var query = "DELETE FROM Companies WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { id });
			}
		}

		public async Task<Company> GetCompanyByEmployeeId(int id)
		{
			var procedureName = "ShowCompanyForProvidedEmployeeId";
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

			using (var connection = _context.CreateConnection())
			{
				var company = await connection.QueryFirstOrDefaultAsync<Company>
					(procedureName, parameters, commandType: CommandType.StoredProcedure);

				return company;
			}
		}

		public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
		{
			var query = "SELECT * FROM Companies WHERE Id = @Id;" +
						"SELECT * FROM Employees WHERE CompanyId = @Id";

			using (var connection = _context.CreateConnection())
			using (var multi = await connection.QueryMultipleAsync(query, new { id }))
			{
				var company = await multi.ReadSingleOrDefaultAsync<Company>();
				if (company != null)
					company.Employees = (await multi.ReadAsync<Employee>()).ToList();

				return company;
			}
		}

		public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
		{
			var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId";

			using (var connection = _context.CreateConnection())
			{
				var companyDict = new Dictionary<int, Company>();

				var companies = await connection.QueryAsync<Company, Employee, Company>(
					query, (company, employee) =>
					{
						if (!companyDict.TryGetValue(company.Id, out var currentCompany))
						{
							currentCompany = company;
							companyDict.Add(currentCompany.Id, currentCompany);
						}

						currentCompany.Employees.Add(employee);
						return currentCompany;
					}
				);

				return companies.Distinct().ToList();
			}
		}

		public async Task CreateMultipleCompanies(List<CompanyForCreationDto> companies)
		{
			var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";

			using (var connection = _context.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					foreach (var company in companies)
					{
						var parameters = new DynamicParameters();
						parameters.Add("Name", company.Name, DbType.String);
						parameters.Add("Address", company.Address, DbType.String);
						parameters.Add("Country", company.Country, DbType.String);

						await connection.ExecuteAsync(query, parameters, transaction: transaction);
						//throw new Exception();
					}

					transaction.Commit();
				}
			}
		}
	}
}
