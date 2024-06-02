using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> OrderingList()
        {
            var result = await _mediator.Send(new GetOrderingQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderingById(int id)
        {
            var result = await _mediator.Send(new GetOrderingByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrdering( CreateOrderingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ordering Added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrdering(UpdateOrderingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ordering Updated");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrdering(int id)
        {
            await _mediator.Send(new RemoveOrderingCommand(id));
            return Ok("Ordering Deleted");
        }
    }
}
