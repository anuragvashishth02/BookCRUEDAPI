using Library.Managers;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllBooks() => Ok(_bookManager.GetAll());

        [HttpGet("{id:guid}")]
        public IActionResult GetBookByID(Guid id)
        {
            var book = _bookManager.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult AddBooks([FromBody] List<AddBookDto> dtos)
        {
            var books = _bookManager.AddBooks(dtos);
            return Ok(books);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateBook(Guid id, [FromBody] UpdateBookDto dto)
        {
            var success = _bookManager.UpdateBook(id, dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBook(Guid id)
        {
            var success = _bookManager.DeleteBook(id);
            return success ? Ok() : NotFound();
        }
    }
}
