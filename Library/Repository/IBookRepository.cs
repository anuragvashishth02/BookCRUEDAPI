using Library.Models.Entities;

namespace Library.Repository
{
    public interface IBookRepository
    {
       Task <List<Books>> GetAllAsync();
       Task <Books?> GetByIdAsync(Guid id);
       Task AddRangeAsync(List<Books> books);
        void Update(Books book);
        void Delete(Books book);
        Task SaveAsync();
    }
}
