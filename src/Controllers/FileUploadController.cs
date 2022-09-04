using DapperASPNetCore.Contracts;
using DapperASPNetCore.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DapperASPNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Headers;

namespace DapperASPNetCore.Controllers
{
    
    [Route("api/fileupload")]
	[ApiController]
	// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class FileUploadController : ControllerBase
	{

        private readonly IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {            
            _fileUploadService = fileUploadService;

        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                              
                Task<string> result =  _fileUploadService.StoreFile(file);

                if (result.Result.Contains("Exception"))
                {
                    return StatusCode(500, result.Result);
                    
                }
                else if (result.Result != "BadRequest")
                {
                    return Ok(result.Result);
                    
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListFiles()
        {        
            try
            {
                var files = await _fileUploadService.GetListFiles();                              

                return Ok(files);

                // return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}