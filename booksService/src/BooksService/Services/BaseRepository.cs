using booksService.Data;
using booksService.Models;
using booksService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksService.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly BookContext _bookContext;

        public BaseRepository(BookContext bookContext) 
        {
            _bookContext = bookContext;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            try
            {
                return await _bookContext.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var item = await _bookContext.Set<T>()
                    .Where(x => x.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();


                if (item == null)
                {
                    throw new Exception($"Couldn't find entity with id={id}");
                }

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with id={id}: {ex.Message}");
            }
        }

        public async Task<T> CreateAsync(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }


            try
            {
                await _bookContext.Set<T>().AddAsync(data);
                await _bookContext.SaveChangesAsync();
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(data)} could not be saved: {ex.Message}");
            }
        }

        public async Task<T> UpdateAsync(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                _bookContext.Set<T>().Update(data);
                await _bookContext.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(data)} could not be updated: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _bookContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"{nameof(id)} could not be found.");
            }

            _bookContext.Set<T>().Remove(entity);
            await _bookContext.SaveChangesAsync();
            return true;
        }

    }
}
