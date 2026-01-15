using Library.Models;
using Library.Models.Entities;

namespace Library.Managers
{
    public interface IBookManager
    {
       Task <List<Books>> GetAllAsync();
       Task < Books?> GetByIdAsync(Guid id);
       Task <List<Books>> AddBooksAsync(List<AddBookDto> books);
       Task <bool> UpdateBookAsync(Guid id, UpdateBookDto dto);
       Task <bool> DeleteBookAsync(Guid id);
    }
}
