using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.UseCases.Movement.Commands.CreateMovement;
using BankAppTestBack.Application.UseCases.Movement.Commands.DeleteMovement;
using BankAppTestBack.Application.UseCases.Movement.Queries.GetAllMovements;
using BankAppTestBack.Application.UseCases.Movement.Queries.GetMovementById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAppTestBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MovementResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovementResponse>> Create(
            [FromBody] CreateMovementCommand request)
        {
            MovementResponse response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { movementId = response.Id }, response);
        }

        [HttpGet("{movementId:long}")]
        [ProducesResponseType(typeof(MovementResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovementResponse>> GetById(long movementId)
        {
            MovementResponse response = await _mediator.Send(new GetMovementByIdQuery(movementId));
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<MovementResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MovementResponse>>> GetAll()
        {
            List<MovementResponse> response = await _mediator.Send(new GetAllMovementsQuery());
            return Ok(response);
        }

        [HttpDelete("{movementId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long movementId)
        {
            await _mediator.Send(new DeleteMovementCommand(movementId));
            return NoContent();
        }
    }
}
