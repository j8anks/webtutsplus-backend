using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
	public class WishListDto
	{
		
		public string EntityId { get; set; }		
		public string CreatedDate { get; set; }
		public long Product { get; set; }		
		public long User { get; set; }
	}
}