using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            Category category = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(category => category.CategoryId == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            List<Category> categories = await _categoryCollection.Find(category => true).ToListAsync();
            List<ResultCategoryDto> resultCategoryDtos = _mapper.Map<List<ResultCategoryDto>>(categories);
            return resultCategoryDtos;
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            Category category = await _categoryCollection.Find<Category>(category => category.CategoryId == id).FirstOrDefaultAsync();
            GetByIdCategoryDto getByIdCategoryDto = _mapper.Map<GetByIdCategoryDto>(category);
            return getByIdCategoryDto;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            Category category = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(c => c.CategoryId == updateCategoryDto.CategoryId, category);
        }
    }
}
