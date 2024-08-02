using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace File.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _filePath = null!;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya yüklenmemiş");
            }

            var rootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(rootPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });

        }

        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            var rootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(rootPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Dosya bulunamadı");
            }

            System.IO.File.Delete(filePath);
            return Ok("Dosya silindi");
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var rootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(rootPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Dosya bulunamadı");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".csv", "text/csv" }
        };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

    }
}
