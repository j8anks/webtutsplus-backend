using Dapper;
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
	public class OrderRepository : IOrderRepository
	{
		private readonly DapperContext _context;

		public OrderRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<OrderDto>> GetOrders()
		{

			var query = @"SELECT name, imageURL, price, description FROM products ";

			using (var connection = _context.CreateConnection())
			{
				var orders = await connection.QueryAsync<OrderDto>(query);
				return orders.ToList();
			}
		}

		public async Task<OrderDto> GetOrder(int id)
		{
			var query = "SELECT * FROM products WHERE Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				var order = await connection.QuerySingleOrDefaultAsync<OrderDto>(query, new { id });

				return order;
			}
		}


		

	}
}
