using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;
using System.Data;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCupponAsync(CreateCupponDto createCupponDto)
        {
            string query = "INSERT INTO Cuppons (Code, Rate, IsActive, ValidDate) " +
                            "VALUES (@Code, @Rate, @IsActive, @ValidDate)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Code", createCupponDto.Code);
            parameters.Add("@Rate", createCupponDto.Rate);
            parameters.Add("@IsActive", createCupponDto.IsActive);
            parameters.Add("@ValidDate", createCupponDto.ValidDate);
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCupponAsync(int id)
        {
            string query = "DELETE FROM Cuppons WHERE CupponId = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCupponDto>> GetAllCupponAsync()
        {
            string query = "SELECT * FROM Cuppons";
            using (IDbConnection connection = _context.CreateConnection())
            {
                IEnumerable<ResultCupponDto> results = await connection.QueryAsync<ResultCupponDto>(query);
                return results.ToList();
            }
        }

        public async Task<GetByIdCupponDto> GetByIdCupponAsync(int id)
        {
            string query = "SELECT * FROM Cuppons WHERE CupponId = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            using (IDbConnection connection = _context.CreateConnection())
            {
                GetByIdCupponDto getByIdCupponDto = await connection.QueryFirstOrDefaultAsync<GetByIdCupponDto>(query, parameters);
                return getByIdCupponDto;
            }
        }

        public async Task UpdateCupponAsync(UpdateCupponDto updateCupponDto)
        {
            string query = "UPDATE Cuppons SET Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate " +
                           "WHERE CupponId = @CupponId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Code", updateCupponDto.Code);
            parameters.Add("@Rate", updateCupponDto.Rate);
            parameters.Add("@IsActive", updateCupponDto.IsActive);
            parameters.Add("@ValidDate", updateCupponDto.ValidDate);
            parameters.Add("@CupponId", updateCupponDto.CupponId);
            using (IDbConnection connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
