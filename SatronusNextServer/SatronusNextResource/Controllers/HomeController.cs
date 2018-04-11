using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
using SatronusNextResource.Models;

namespace SatronusNextResource.Controllers
{
    [Route("resource/[controller]")]
    public class HomeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            foreach (var file in HttpContext.Request.Form.Files)
            {
                if (FileManagement.IsError(file.FileName))
                    return Content("File not upload");

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot",
                            User.FindFirst("email")?.Value);
                FileManagement.DirectoryValidation(path);

                using (var stream = new FileStream(path + file.FileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Content("File upload");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(string filename)
        {
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", User.FindFirst("email")?.Value, filename);

            if (FileManagement.FileValidation(filename))
                return Content("filename not present");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, path, Path.GetFileName(path));
        }
    }
}