using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProducImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _ProductImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _ProductImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            ProductImage productImage = _mapper.Map<ProductImage>(createProductImageDto);
            await _ProductImageCollection.InsertOneAsync(productImage);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _ProductImageCollection.DeleteOneAsync(ProductImage => ProductImage.ProductImageId == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            List<ProductImage> categories = await _ProductImageCollection.Find(ProductImage => true).ToListAsync();
            List<ResultProductImageDto> resultProductImageDtos = _mapper.Map<List<ResultProductImageDto>>(categories);
            return resultProductImageDtos;
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            ProductImage productImage = await _ProductImageCollection.Find<ProductImage>(ProductImage => ProductImage.ProductImageId == id).FirstOrDefaultAsync();
            GetByIdProductImageDto getByIdProductImageDto = _mapper.Map<GetByIdProductImageDto>(productImage);
            return getByIdProductImageDto;
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            ProductImage ProductImage = _mapper.Map<ProductImage>(updateProductImageDto);
            await _ProductImageCollection.FindOneAndReplaceAsync(c => c.ProductImageId == updateProductImageDto.ProductImageId, ProductImage);
        }
    }
}
