using BankAppTestBack.Domain.Entities;

namespace BankAppTestBack.Application.Dtos
{
    public record AccountResponse(
        long Id,
        string Number,
        EAccountType Type,
        double InitBalance,
        bool State,
        long ClientId,
        string clientName
    );
}
