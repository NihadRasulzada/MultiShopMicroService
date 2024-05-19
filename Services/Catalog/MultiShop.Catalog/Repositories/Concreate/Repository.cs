using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstraction;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concreate
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IDatabaseSettings databaseSettings)
        {
            new MongoClient(databaseSettings.ConnectionString)
                .GetDatabase(databaseSettings.DatabaseName)
                .GetCollection<T>(nameof(T) + "CollectionName");
        }
        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> values = await _collection.Find(x => true).ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            T value = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return value;
        }

        public async Task UpdateAsync(T endtiy)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == endtiy.Id, endtiy);
        }
    }
}
