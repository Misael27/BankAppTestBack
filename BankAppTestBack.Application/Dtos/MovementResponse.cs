using BankAppTestBack.Domain.Entities;

namespace BankAppTestBack.Application.Dtos
{
    public record MovementResponse(
        long Id,
        DateTime Date,
        EMovementType Type,
        double Value,
        double Balance,
        long AccountId,
        string accountNumber
    );
}