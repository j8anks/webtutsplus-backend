using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Stripe;

namespace DapperASPNetCore.Controllers
{
	[Route("api/order")]
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepo;
		private readonly IOrderService _orderService;

		public OrderController(IOrderRepository orderRepo, IOrderService orderService)
		{
			_orderRepo = orderRepo;
			_orderService = orderService;
		}

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CheckoutList(List<CheckoutItemDto> checkoutItemDtoList)
        {
            try
            {				
				// create the stripe session
				Session session = _orderService.CreateSession(checkoutItemDtoList);
				// StripeResponse stripeResponse = new StripeResponse(session.getId());

				var checkoutList = checkoutItemDtoList; // await _orderRepo.GetProducts();

                return Ok();

                
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetOrders")]
		public async Task<IActionResult> GetAllOrders()
		{
			try
			{
				var orders = await _orderRepo.GetOrders();
				if (orders == null)
					return NotFound();

				return Ok(orders);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetOrderById")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			try
			{
				var product = await _orderRepo.GetOrder(id);
				if (product == null)
					return NotFound();

				return Ok(product);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}


		//[HttpPost]
		//public async Task<IActionResult> CreateProduct(ProductForCreationDto product)
		//{
		//	try
		//	{
		//		var createdProduct = await _orderRepo.CreateProduct(product);
  //              return CreatedAtRoute("ProductById", new { id = createdProduct.Id }, createdProduct);
		//	}
		//	catch (Exception ex)
		//	{
		//		//log error
		//		return StatusCode(500, ex.Message);
		//	}
		//}

		//[HttpPut("{id}")]
		//public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto product)
		//{
		//	try
		//	{
		//		var dbProduct = await _orderRepo.GetProduct(id);
		//		if (dbProduct == null)
		//			return NotFound();

		//		await _orderRepo.UpdateProduct(id, product);
		//		return NoContent();
		//	}
		//	catch (Exception ex)
		//	{
		//		//log error
		//		return StatusCode(500, ex.Message);
		//	}
		//}

		//[HttpDelete("{id}")]
		//public async Task<IActionResult> DeleteProduct(int id)
		//{
		//	try
		//	{
		//		var dbProduct = await _orderRepo.GetProduct(id);
		//		if (dbProduct == null)
		//			return NotFound();

		//		await _orderRepo.DeleteCompany(id);
		//		return NoContent();
		//	}
		//	catch (Exception ex)
		//	{
		//		//log error
		//		return StatusCode(500, ex.Message);
		//	}
		//}
	}
}
