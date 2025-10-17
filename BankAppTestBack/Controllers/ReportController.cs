using BankAppTestBack.Application.UseCases.Reports.Queries.GetMovementReport;
using BankAppTestBack.Domain.Projections;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAppTestBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("movement-report")]
        [ProducesResponseType(typeof(List<MovementReport>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<MovementReport>>> GetMovementReport(
            [FromQuery] GetMovementReportQuery query)
        {
            List<MovementReport> response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
