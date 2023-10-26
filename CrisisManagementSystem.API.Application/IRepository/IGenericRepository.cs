namespace CrisisManagementSystem.API.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get a single record of the T collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to retrieve.</param>
        /// <returns>A Task representing the asynchronous operation. The result is the record of type T with the specified ID.</returns>
        Task<T> GetAsync(int? id);
        /// <summary>
        /// Get all records of the T collection.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation. The result is a list of records of type T.</returns>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Add a record of type T to the collection.
        /// </summary>
        /// <param name="entity">The record to be added.</param>
        /// <returns>A Task representing the asynchronous operation. The result is the added record of type T.</returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Delete a record from the collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task DeleteAsync(int? id);
        /// <summary>
        /// Update a record of type T in the collection.
        /// </summary>
        /// <param name="entity">The record to be updated.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Check if a record with the specified ID exists in the collection.
        /// </summary>
        /// <param name="id">The ID of the record to check for existence.</param>
        /// <returns>A Task representing the asynchronous operation. The result is a boolean indicating whether the record exists (true) or not (false).</returns>
        Task<bool> Exists(int id);


    }
}
