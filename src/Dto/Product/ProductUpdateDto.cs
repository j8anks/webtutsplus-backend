using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
	public class ProductUpdateDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageURL { get; set; }
		public double Price { get; set; }		
		public string CategoryId { get; set; }
	}
}
