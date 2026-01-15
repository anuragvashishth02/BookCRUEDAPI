using Library.Managers;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookManager _bookManager;

        public BooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookManager.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookByID(Guid id)
        {
            var book = await _bookManager.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooks([FromBody] List<AddBookDto> dtos)
        {
            var books = await _bookManager.AddBooksAsync(dtos);
            return Ok(books);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto dto)
        {
            var success = await _bookManager.UpdateBookAsync(id, dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var success = await _bookManager.DeleteBookAsync(id);
            return success ? Ok() : NotFound();
        }

        [HttpPost("upload-base64")]
        public async Task<IActionResult> UploadBookFileBase64([FromBody] UploadBase64FileDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FileName) || string.IsNullOrWhiteSpace(dto.Base64Content))
                return BadRequest("Invalid input.");

            var fileBytes = Convert.FromBase64String(dto.Base64Content);

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedBooks");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, dto.FileName);

            await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

            return Ok(new { FileName = dto.FileName, Message = "Base64 file uploaded successfully." });
        }

        [HttpGet("download-base64/{fileName}")]
        public async Task<IActionResult> DownloadBookFileBase64(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedBooks", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var base64 = Convert.ToBase64String(fileBytes);

            var response = new DownloadBase64FileDto
            {
                FileName = fileName,
                Base64Content = base64
            };

            return Ok(response);
        }

    }
}
