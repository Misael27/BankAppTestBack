using BankAppTestBack.Domain.Entities;

namespace BankAppTestBack.Domain.Projections
{
    public record MovementReport(
        long Id,
        DateTime Date,
        string Name,
        string Number,
        EAccountType Type,
        bool State,
        double InitBalance,
        double Movement,
        double Balance
    );
}