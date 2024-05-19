using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultCupponDto>> GetAllCupponAsync();
        Task CreateCupponAsync(CreateCupponDto createCupponDto);
        Task UpdateCupponAsync(UpdateCupponDto updateCupponDto);
        Task DeleteCupponAsync(int id);
        Task<GetByIdCupponDto> GetByIdCupponAsync(int id);
    }
}
