using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;

namespace DapperASPNetCore.Controllers
{
	[Route("api/")]	
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepo;

		public ProductController(IProductRepository productRepo)
		{
			_productRepo = productRepo;
		}

		[HttpGet("product")]
		public async Task<IActionResult> GetProducts()
		{

			try
			{
				var products = await _productRepo.GetProducts();

				return Ok(products);

				
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		
		//[HttpGet("product/{id}", Name = "ProductById")]
		//public async Task<IActionResult> GetProduct(int id)
		//{
		//	try
		//	{
		//		var product = await _productRepo.GetProduct(id);
		//		if (product == null)
		//			return NotFound();

		//		return Ok(product);
		//	}
		//	catch (Exception ex)
		//	{
		//		//log error
		//		return StatusCode(500, ex.Message);
		//	}
		//}

		
		[HttpPost("product/add")]
		public async Task<IActionResult> CreateProduct(ProductForCreationDto product)
		{
			
			try
			{
                 var createdProduct = await _productRepo.CreateProduct(product);
                // return CreatedAtRoute("ProductById", new { id = createdProduct.Id }, createdProduct);
				return StatusCode(200, "product added");
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("product/update/{id:int}")]
		public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto product)
		{
			

			try
			{
				var dbProduct = await _productRepo.GetProduct(id);
				if (dbProduct == null)
					return NotFound();

				await _productRepo.UpdateProduct(id, product);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("product/delete/{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				var dbProduct = await _productRepo.GetProduct(id);
				if (dbProduct == null)
					return NotFound();

				await _productRepo.DeleteCompany(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}
	}
}
