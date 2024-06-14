using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler _getAddressQueryHandler;
        private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler _createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
        private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

        public AddressesController(GetAddressQueryHandler getAddressQueryHandler,
            GetAddressByIdQueryHandler getAddressByIdQueryHandler,
            CreateAddressCommandHandler createAddressCommandHandler,
            UpdateAddressCommandHandler updateAddressCommandHandler,
            RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            _getAddressQueryHandler = getAddressQueryHandler;
            _getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            _createAddressCommandHandler = createAddressCommandHandler;
            _updateAddressCommandHandler = updateAddressCommandHandler;
            _removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<ActionResult> AddressList()
        {
            IEnumerable<GetAddressQueryResult> result = await _getAddressQueryHandler.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> AddressListById(int id)
        {
            GetAddressByIdQueryResult result = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress(CreateAddressCommand command)
        {
            await _createAddressCommandHandler.Handle(command);
            return Ok("Address Added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await _updateAddressCommandHandler.Handle(command);
            return Ok("Address Updated");
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveAddress(int id)
        {
            await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return Ok("Address Deleted");
        }
    }
}
