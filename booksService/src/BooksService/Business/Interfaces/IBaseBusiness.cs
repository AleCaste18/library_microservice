using booksService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace booksService.Business.Interfaces
{
    public interface IBaseBusiness<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
    }
}
