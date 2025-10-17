using BankAppTestBack.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankAppTestBack.Application.UseCases.Account.Commands.UpdateAccount
{
    public record UpdateAccountRequest
    {
        public string? Number { get; init; }

        public EAccountType Type { get; init; }

        [Range(0, double.MaxValue, ErrorMessage = "El saldo inicial no puede ser negativo.")]
        public double InitBalance { get; init; }

        public bool State { get; init; }
    }
}