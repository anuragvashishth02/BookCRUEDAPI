using Library.Models.Entities;

namespace Library.Repository
{
    public interface IBookRepository
    {
        List<Books> GetAll();
        Books? GetById(Guid id);
        void AddRange(List<Books> books);
        void Update(Books book);
        void Delete(Books book);
        void Save();
    }
}
