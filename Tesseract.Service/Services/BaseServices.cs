using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tesseract.Domain.Entities;
using Tesseract.Domain.Interfaces;

namespace Tesseract.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task DeleteAsync(long id) => await _baseRepository.DeleteAsync(id);

        public async Task<T> GetAsync(long id) => await _baseRepository.GetAsync(id);

        public async Task<IEnumerable<T>> GetListAsync() => await _baseRepository.GetListAsync();

        public async Task<T> InsertAsync<TValidator>(T obj) where TValidator : AbstractValidator<T>
        {
            await ValidateAsync(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.InsertAsync(obj);
            return obj;
        }

        public async Task<T> UpdateAsync<TValidator>(T obj) where TValidator : AbstractValidator<T>
        {
            await ValidateAsync(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.UpdateAsync(obj);
            return obj;
        }

        private async Task ValidateAsync(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Records not detected!");

            await validator.ValidateAndThrowAsync(obj);
        }
    }
}
