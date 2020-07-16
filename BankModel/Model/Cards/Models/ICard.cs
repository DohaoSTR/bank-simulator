using MaterialDesignThemes.Wpf;
using System;
using Test.Cards.Models;

namespace Test.Operations.Cards.Models
{
    public interface ICard
    {
        bool Open();
        bool Close();
        bool Withdrawal(decimal sumWithdrawal);
        bool AnnualPayment();
        ICardModel CardModel { get; set; }
        int Id { get; set; }
        string Password { get; set; }
        DateTime ValidityEnd { get; }
    }
}
