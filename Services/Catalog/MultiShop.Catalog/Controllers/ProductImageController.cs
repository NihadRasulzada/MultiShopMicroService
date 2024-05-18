using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProducImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _ProductImageService;

        public ProductImageController(IProductImageService ProductImageService)
        {
            _ProductImageService = ProductImageService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            List<ResultProductImageDto> resultProductImageDtos = await _ProductImageService.GetAllProductImageAsync();
            return Ok(resultProductImageDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            GetByIdProductImageDto getByIdProductImageDto = await _ProductImageService.GetByIdProductImageAsync(id);
            return Ok(getByIdProductImageDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            await _ProductImageService.CreateProductImageAsync(createProductImageDto);
            return Ok("ProductImage Created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            await _ProductImageService.UpdateProductImageAsync(updateProductImageDto);
            return Ok("ProductImage Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await _ProductImageService.DeleteProductImageAsync(id);
            return Ok("ProductImage Deleted");
        }
    }
}
