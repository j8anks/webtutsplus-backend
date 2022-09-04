using DapperASPNetCore.Services;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DapperASPNetCore.Controllers
{
	[Route("api/wishlist")]
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class WishListController : ControllerBase
	{
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService, IAccountService accountService)
		{
              _wishListService = wishListService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetWishList([FromRoute] string token) {
           
            var userClaim = WebApplication.JwtHelpers.JwtHelpers.ValidateJwtToken(token);
            
            var body = await _wishListService.GetWishList(userClaim);            ;

            return Ok(body); 
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWishList(string token, WishListForCreationDto wishlistForCreationDto)
        {
            var userClaim = WebApplication.JwtHelpers.JwtHelpers.ValidateJwtToken(token);

            if (userClaim.UserId == null)
            {
                return BadRequest();
            }

            var result = await _wishListService.AddWishList(userClaim, wishlistForCreationDto);

            return StatusCode(201);

        }
    }

}

