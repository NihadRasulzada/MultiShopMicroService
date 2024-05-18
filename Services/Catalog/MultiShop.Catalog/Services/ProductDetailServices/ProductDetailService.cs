using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductsDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> _ProductDetailCollection;
        private readonly IMapper _mapper;

        public ProductDetailService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _ProductDetailCollection = database.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            ProductDetail productDetail = _mapper.Map<ProductDetail>(createProductDetailDto);
            await _ProductDetailCollection.InsertOneAsync(productDetail);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _ProductDetailCollection.DeleteOneAsync(ProductDetail => ProductDetail.ProductDetailId == id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            List<ProductDetail> categories = await _ProductDetailCollection.Find(ProductDetail => true).ToListAsync();
            List<ResultProductDetailDto> resultProductDetailDtos = _mapper.Map<List<ResultProductDetailDto>>(categories);
            return resultProductDetailDtos;
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            ProductDetail productDetail = await _ProductDetailCollection.Find<ProductDetail>(ProductDetail => ProductDetail.ProductDetailId == id).FirstOrDefaultAsync();
            GetByIdProductDetailDto getByIdProductDetailDto = _mapper.Map<GetByIdProductDetailDto>(productDetail);
            return getByIdProductDetailDto;
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            ProductDetail ProductDetail = _mapper.Map<ProductDetail>(updateProductDetailDto);
            await _ProductDetailCollection.FindOneAndReplaceAsync(c => c.ProductDetailId == updateProductDetailDto.ProductDetailId, ProductDetail);
        }
    }
}
