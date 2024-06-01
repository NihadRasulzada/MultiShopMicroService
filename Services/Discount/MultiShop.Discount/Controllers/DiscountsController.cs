using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> CupponList()
        {
            List<ResultCupponDto> resultCupponDtos = await _discountService.GetAllCupponAsync();
            return Ok(resultCupponDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCupponById(int id)
        {
            GetByIdCupponDto getByIdCupponDto = await _discountService.GetByIdCupponAsync(id);
            return Ok(getByIdCupponDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCuppon(CreateCupponDto createCupponDto)
        {
            await _discountService.CreateCupponAsync(createCupponDto);
            return Ok("Cuppon created");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCuppon(int id)
        {
            await _discountService.DeleteCupponAsync(id);
            return Ok("Cuppon deleted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCuppon(UpdateCupponDto updateCupponDto)
        {
            await _discountService.UpdateCupponAsync(updateCupponDto);
            return Ok("Cuppon updated");
        }
    }
}
