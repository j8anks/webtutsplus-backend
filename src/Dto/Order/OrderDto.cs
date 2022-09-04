using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Dto
{
	public class OrderDto
	{
        public string order_item_id { get; set; }
        public string created_date { get; set; }
        public string order_id { get; set; }
        public string price { get; set; }
        public string product_id { get; set; }
        public string quantity { get; set; }

    }
}
