using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NgNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = file.FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }

  
        }

        [HttpPost("UploadByte")]
        public IActionResult UploadByte()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        byte[] Content = memoryStream.ToArray();
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }

          
        }

        [HttpPost("UploadProfilePicture")]
        public async Task<ActionResult<string>> UploadProfilePicture([FromForm(Name = "file")] IFormFile file, long userId)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] Content = memoryStream.ToArray();
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("UploadMultiplesProfilePicture")]
        public async Task<ActionResult<string>> UploadMultiplesProfilePicture([FromForm(Name = "file")] IFormFileCollection files, long userId)
        {
            try
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byte[] Content = memoryStream.ToArray();
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch 
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}