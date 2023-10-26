using CrisisManagementSystem.API.DataLayer;
using CrisisManagementSystem.API.Application.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CrisisManagementSystem.API.Application.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CrisisManagementDbContext _context;

        public GenericRepository(CrisisManagementDbContext context)
        {
           _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity); //remove dont have async method
           await  _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity =  await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            //From the database, get the DBSet<> assoicated with T
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
