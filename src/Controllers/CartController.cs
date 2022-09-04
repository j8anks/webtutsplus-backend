using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using DapperASPNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DapperASPNetCore.Controllers
{
	[Route("api/cart")]
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class CartController : ControllerBase
	{
        private readonly IAccountService _accountService;
        private readonly ICartRepository _cartRepo;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartRepository cartRepo, ICartService cartService, IAccountService accountService)
		{
			_cartRepo = cartRepo;
            _cartService = cartService;
            _accountService = accountService;

        }


        // https://stackoverflow.com/questions/15689989/connecting-api-controller-with-service-layer
        // private CartService _cartService = new CartService();
        // Do the following instead


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto, [FromQuery] string token)
        {
            var userClaim = WebApplication.JwtHelpers.JwtHelpers.ValidateJwtToken(token);

            if (userClaim.UserId == null)
            {
                return BadRequest();
            }

            var result = await _cartService.AddToCart(userClaim, addToCartDto);
                       
            return StatusCode(201);          
        }

        [HttpGet()]
        public async Task<IActionResult> GetCartItems([FromQuery] string token)
        {
            var userClaim = WebApplication.JwtHelpers.JwtHelpers.ValidateJwtToken(token);

            if (userClaim.UserId == null)
            {
                return BadRequest();
            }

            var cartList = await _cartService.ListCartItems(userClaim);
                       

            return Ok(cartList);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] string token, string id)
        {
            var userClaim = WebApplication.JwtHelpers.JwtHelpers.ValidateJwtToken(token);

            Int64 cartId = 0;
            Int64.TryParse(id, out cartId);

            var deleteFromCart = await _cartService.DeleteCartItem(userClaim, cartId);

            return Ok(deleteFromCart);

        }

        //    [SwaggerOperation(Summary = "Write your summary here")]
        //    [HttpGet("{id}", Name = "GetCartItem")]
        //    public async Task<IActionResult> GetCartItems(int id)
        //    {
        //        try
        //        {
        //            var company = await _cartRepo.GetCartItem(id);
        //            if (company == null)
        //                return NotFound();

        //            return Ok(company);
        //        }
        //        catch (Exception ex)
        //        {
        //            //log error
        //            return StatusCode(500, ex.Message);
        //        }
        //    }

        //    [HttpPut("update/{id}")]
        //    public async Task<IActionResult> UpdateCartItem(int id, AddToCartDto addToCartDto)
        //    {            
        //        try
        //        {
        //            var dbCartItemUpdate = await _cartRepo.GetCartItem(id);
        //            if (dbCartItemUpdate == null)
        //                return NotFound();

        //            await _cartRepo.UpdateCartItem(id, addToCartDto);
        //            return NoContent();
        //        }
        //        catch (Exception ex)
        //        {
        //            //log error
        //            return StatusCode(500, ex.Message);
        //        }
        //    }


        //    [HttpDelete("delete/{id}")]
        //    public async Task<IActionResult> DeleteCartItem(int id)
        //    {
        //        try
        //        {
        //            var dbDeleteCartItem = await _cartRepo.GetCartItem(id);
        //            if (dbDeleteCartItem == null)
        //                return NotFound();

        //            await _cartRepo.DeleteCartItem(id);
        //            return NoContent();
        //        }
        //        catch (Exception ex)
        //        {
        //            //log error
        //            return StatusCode(500, ex.Message);
        //        }
        //    }
    }
}
