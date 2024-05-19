using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T endtiy);
    }
}
