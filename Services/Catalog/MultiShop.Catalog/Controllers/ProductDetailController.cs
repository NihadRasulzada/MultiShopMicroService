using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductsDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailService _ProductDetailService;

        public ProductDetailController(IProductDetailService ProductDetailService)
        {
            _ProductDetailService = ProductDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            List<ResultProductDetailDto> resultProductDetailDtos = await _ProductDetailService.GetAllProductDetailAsync();
            return Ok(resultProductDetailDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            GetByIdProductDetailDto getByIdProductDetailDto = await _ProductDetailService.GetByIdProductDetailAsync(id);
            return Ok(getByIdProductDetailDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            await _ProductDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("ProductDetail Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await _ProductDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("ProductDetail Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _ProductDetailService.DeleteProductDetailAsync(id);
            return Ok("ProductDetail Deleted");
        }
    }
}
