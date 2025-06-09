using Library.Models;
using Library.Models.Entities;

namespace Library.Managers
{
    public interface IBookManager
    {
        List<Books> GetAll();
        Books? GetById(Guid id);
        List<Books> AddBooks(List<AddBookDto> books);
        bool UpdateBook(Guid id, UpdateBookDto dto);
        bool DeleteBook(Guid id);
    }
}
