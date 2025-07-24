using EmployeeManagement.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Departments
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<T?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(T entity) => await _repository.AddAsync(entity);

        public async Task UpdateAsync(T entity)
        {
           await _repository.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(object id) => await _repository.DeleteAsync(id);

        public async Task<bool> ExistsAsync(object id) => await _repository.ExistsAsync(id);
    }
}
