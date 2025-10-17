using BankAppTestBack.Application.Account.Queries.BankAppTestBack.Application.Account.Queries.GetAccountById;
using BankAppTestBack.Application.Dtos;
using BankAppTestBack.Application.UseCases.Account.Commands.CreateAccount;
using BankAppTestBack.Application.UseCases.Account.Commands.DeleteAccount;
using BankAppTestBack.Application.UseCases.Account.Commands.UpdateAccount;
using BankAppTestBack.Application.UseCases.Account.Queries.GetAllAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAppTestBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponse>> Create(
            [FromBody] CreateAccountCommand request)
        {
            AccountResponse response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { accountId = response.Id }, response);
        }

        [HttpGet("{accountId:long}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountResponse>> GetById(long accountId)
        {
            AccountResponse response = await _mediator.Send(new GetAccountByIdQuery(accountId));
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AccountResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountResponse>>> GetAll()
        {
            List<AccountResponse> response = await _mediator.Send(new GetAllAccountsQuery());
            return Ok(response);
        }

        [HttpPut("{accountId:long}")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponse>> Update(
            long accountId,
            [FromBody] UpdateAccountRequest request)
        {
            var command = new UpdateAccountCommand
            {
                AccountId = accountId,
                Number = request.Number!,
                Type = request.Type,
                InitBalance = request.InitBalance,
                State = request.State
            };

            AccountResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{accountId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long accountId)
        {
            await _mediator.Send(new DeleteAccountCommand(accountId));
            return NoContent();
        }
    }
}
