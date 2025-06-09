using Library.Models;
using Library.Models.Entities;
using Library.Repository;

namespace Library.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _repo;

        public BookManager(IBookRepository repo)
        {
            _repo = repo;
        }

        public List<Books> GetAll() => _repo.GetAll();

        public Books? GetById(Guid id) => _repo.GetById(id);

        public List<Books> AddBooks(List<AddBookDto> dtos)
        {
            var books = dtos.Select(dto => new Books
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description
            }).ToList();

            _repo.AddRange(books);
            _repo.Save();
            return books;
        }

        public bool UpdateBook(Guid id, UpdateBookDto dto)
        {
            var book = _repo.GetById(id);
            if (book is null) return false;

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Description = dto.Description;

            _repo.Update(book);
            _repo.Save();
            return true;
        }

        public bool DeleteBook(Guid id)
        {
            var book = _repo.GetById(id);
            if (book is null) return false;

            _repo.Delete(book);
            _repo.Save();
            return true;
        }
    }
}
