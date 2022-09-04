using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public string ImageURL { get; set; }
		public List<ProductDto> Products { get; set; }
	}
}
