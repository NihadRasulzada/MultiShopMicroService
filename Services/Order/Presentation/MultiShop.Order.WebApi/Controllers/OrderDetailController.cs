using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetaliResults;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
        private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
        private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
        private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;

        public OrderDetailController(GetOrderDetailQueryHandler getOrderDetailQueryHandler,
            GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler,
            CreateOrderDetailCommandHandler createOrderDetailCommandHandler,
            UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler,
            RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
        {
            _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
            _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
            _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> OrderDetailList()
        {
            List<GetOrderDetailQueryResult> results = await _getOrderDetailQueryHandler.Handle();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderDetailById(int id)
        {
            GetOrderDetailByIdQueryResult result = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {
            await _createOrderDetailCommandHandler.Handle(command);
            return Ok("OrderDetail Added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            await _updateOrderDetailCommandHandler.Handle(command);
            return Ok("Order Detail Updated");
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveOrderDetail(int id)
        {
            await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
            return Ok("Order Detail Removed");
        }
    }
}
