using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
	public class ProductForCreationDto
	{		
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageURL { get; set; }
		public double Price { get; set; }		
		public int CategoryId { get; set; }
	}
}
