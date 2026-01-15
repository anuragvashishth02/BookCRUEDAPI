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

        public async Task <List<Books>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task <Books?> GetByIdAsync(Guid id) => await _repo.GetByIdAsync(id);

        public async Task <List<Books>> AddBooksAsync(List<AddBookDto> dtos)
        {
            var books = dtos.Select(dto => new Books
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description
            }).ToList();

            await _repo.AddRangeAsync(books);
            await _repo.SaveAsync();
            return books;
        }

        public async Task <bool> UpdateBookAsync(Guid id, UpdateBookDto dto)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book is null) return false;

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Description = dto.Description;

            _repo.Update(book);
            await _repo.SaveAsync();
            return true;
        }

        public async Task <bool> DeleteBookAsync(Guid id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book is null) return false;

            _repo.Delete(book);
            await _repo.SaveAsync();
            return true;
        }
    }
}
