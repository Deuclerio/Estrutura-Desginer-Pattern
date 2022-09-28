using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesseract.Domain.Entities;

namespace Tesseract.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> GetListAsync();
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
