using Library.Data;
using Library.Models.Entities;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Books> GetAll() => _context.Books.ToList();

        public Books? GetById(Guid id) => _context.Books.Find(id);

        public void AddRange(List<Books> books) => _context.Books.AddRange(books);

        public void Update(Books book) => _context.Books.Update(book);

        public void Delete(Books book) => _context.Books.Remove(book);

        public void Save() => _context.SaveChanges();
    }
}
