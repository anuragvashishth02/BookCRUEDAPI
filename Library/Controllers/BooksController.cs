using Library.Data;
using Library.Models;
using Library.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var allbooks = dbContext.Books.ToList();
            return Ok(allbooks);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public IActionResult GetBookByID(Guid id)
        {
          var books = dbContext.Books.Find(id);

            if(books is null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBooks([FromBody] List<AddBookDto> addBookDtos)
        {
            var bookEntities = addBookDtos.Select(dto => new Books
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description
            }).ToList();

            dbContext.Books.AddRange(bookEntities);
            dbContext.SaveChanges();

            return Ok(bookEntities);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateBook(Guid id,UpdateBookDto updateBookDto)
        {
            var book = dbContext.Books.Find(id);

            if (book is null)
            {
                return NotFound();
            }

            book.Title = updateBookDto.Title;
            book.Author = updateBookDto.Author;
            book.Description = updateBookDto.Description;

            dbContext.SaveChanges();

            return Ok();
               
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteBook(Guid id)
        {
            var book= dbContext.Books.Find(id);

            if(book is null)
            {
                return NotFound(); 
            }
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();

            return Ok();
        } 
    }
}
