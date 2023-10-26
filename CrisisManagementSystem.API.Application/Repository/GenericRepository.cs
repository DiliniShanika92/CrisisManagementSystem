using CrisisManagementSystem.API.DataLayer;
using CrisisManagementSystem.API.Application.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CrisisManagementSystem.API.Application.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CrisisManagementDbContext _context;
        /// <summary>
        /// Initializes a new instance of the GenericRepository class with the specified database context.
        /// </summary>
        /// <param name="context">The CrisisManagementDbContext to be used by the repository.</param>
        public GenericRepository(CrisisManagementDbContext context)
        {
           _context = context;
        }
        /// <summary>
        /// Add a record of type T to the collection.
        /// </summary>
        /// <param name="entity">The record to be added.</param>
        /// <returns>A Task representing the asynchronous operation. The result is the added record of type T.</returns>
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Delete a record from the collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int? id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity); //remove dont have async method
           await  _context.SaveChangesAsync();
        }
        /// <summary>
        /// Check if a record with the specified ID exists in the collection.
        /// </summary>
        /// <param name="id">The ID of the record to check for existence.</param>
        /// <returns>A Task representing the asynchronous operation. The result is a boolean indicating whether the record exists (true) or not (false).</returns>
        public async Task<bool> Exists(int id)
        {
            var entity =  await GetAsync(id);
            return entity != null;
        }

        /// <summary>
        /// Get all records of the T collection.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation. The result is a list of records of type T.</returns>
        public async Task<List<T>> GetAllAsync()
        {
            //From the database, get the DBSet<> assoicated with T
            return await _context.Set<T>().ToListAsync();

        }
        /// <summary>
        /// Get a single record of the T collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to retrieve.</param>
        /// <returns>A Task representing the asynchronous operation. The result is the record of type T with the specified ID.</returns>
        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Update a record of type T in the collection.
        /// </summary>
        /// <param name="entity">The record to be updated.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
