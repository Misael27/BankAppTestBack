using BankAppTestBack.Application.Client.Queries.GetAllClients.BankAppTestBack.Application.Client.Queries.GetAllClients;
using BankAppTestBack.Application.Client.Queries.GetClientById.BankAppTestBack.Application.Client.Queries.GetClientById;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.UseCases.Client.Commands.CreateClient;
using BankAppTestBack.Application.UseCases.Client.Commands.DeleteClient;
using BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAppTestBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientResponse>> Create(
            [FromBody] CreateClientCommand request)
        {
            ClientResponse response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { clientId = response.Id }, response);
        }

        [HttpGet("{clientId:long}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientResponse>> GetById(long clientId)
        {
            ClientResponse response = await _mediator.Send(new GetClientByIdQuery(clientId));
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientResponse>>> GetAll()
        {
            List<ClientResponse> response = await _mediator.Send(new GetAllClientsQuery());
            return Ok(response);
        }

        [HttpPut("{clientId:long}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientResponse>> Update(
            long clientId,
            [FromBody] UpdateClientRequest request)
        {
            var command = new UpdateClientCommand
            {
                ClientId = clientId,
                Name = request.Name,
                Gender = request.Gender,
                Birthdate = request.Birthdate,
                PersonId = request.PersonId,
                NewPassword = request.Password,
                State = request.State,
                Address = request.Address,
                Phone = request.Phone
            };

        ClientResponse response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete("{clientId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long clientId)
        {
            await _mediator.Send(new DeleteClientCommand(clientId));
            return NoContent();
        }
    }
}
