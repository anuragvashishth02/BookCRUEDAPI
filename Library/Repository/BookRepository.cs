using Library.Data;
using Library.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Books>> GetAllAsync() => await _context.Books.ToListAsync();
        public async Task <Books?> GetByIdAsync(Guid id) => await _context.Books.FindAsync(id);

        public async Task AddRangeAsync(List<Books> books) => await _context.Books.AddRangeAsync(books);

        public void Update(Books book) => _context.Books.Update(book);

        public void Delete(Books book) => _context.Books.Remove(book);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
