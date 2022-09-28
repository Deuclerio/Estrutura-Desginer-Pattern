using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tesseract.Repository.Context;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;

namespace Tesseract.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public readonly DbContext _context;

        public BaseRepository(OcrContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long id)
        {

            _context.Set<T>().Remove(await GetAsync(id)); ;
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
