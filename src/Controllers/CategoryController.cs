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

namespace DapperASPNetCore.Controllers
{
	[Route("api/category")]
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepo;

		public CategoryController(ICategoryRepository categoryRepo)
		{
			_categoryRepo = categoryRepo;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{

			try
			{
				var categories = await _categoryRepo.GetCategories();

				return Ok(categories);
				
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "CategoryById")]
		public async Task<IActionResult> GetCategory(int id)
		{
			try
			{
				var category = await _categoryRepo.GetCategory(id);
				if (category == null)
					return NotFound();

				return Ok(category);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("create")] 		
		public async Task<IActionResult> CreateCategory(CategoryForCreationDto category)
		{
			try
			{
				var createdCategory = await _categoryRepo.CreateCategory(category);
                return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost("update/{id}")]
		public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto category)
		{
			try
			{
				var dbCategory = await _categoryRepo.GetCategory(id);
				if (dbCategory == null)
					return NotFound();

				await _categoryRepo.UpdateCategory(id, category);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			try
			{
				var dbCategory = await _categoryRepo.GetCategory(id);
				if (dbCategory == null)
					return NotFound();

				await _categoryRepo.DeleteCategory(id);
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
