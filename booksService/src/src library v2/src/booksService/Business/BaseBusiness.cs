using booksService.Business.Interfaces;
using booksService.Models;
using booksService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace booksService.Business
{
    public class BaseBusiness<TRepository, TEntity> : IBaseBusiness<TEntity> where TRepository : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> Repository;
        protected readonly ILogger Logger;

        public BaseBusiness(TRepository repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public Task<IList<TEntity>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return Repository.GetByIdAsync(id);
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            return Repository.CreateAsync(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Repository.UpdateAsync(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Repository.DeleteAsync(id);
        }
    }
}
