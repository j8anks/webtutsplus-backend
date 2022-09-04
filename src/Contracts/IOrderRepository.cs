using DapperASPNetCore.Dto;
using DapperASPNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{

	public interface IOrderRepository
	{
		public Task<IEnumerable<OrderDto>> GetOrders();
		public Task<OrderDto> GetOrder(int id);


	}
}
