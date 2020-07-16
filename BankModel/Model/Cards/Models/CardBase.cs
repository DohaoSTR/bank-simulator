using MaterialDesignThemes.Wpf;
using System;
using Test.Cards.Models;
using Test.Persons.Models;

namespace Test.Operations.Cards.Models
{
    abstract public class CardBase : ICard
    {
        protected IPerson CurrentPerson { get; set; }
        public ICardModel CardModel { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd
        {
            get
            {
                return DateTime.Now.AddYears(CardModel.ValidityYears);
            }
        }

        public abstract bool Open();
        public abstract bool Close();
        public abstract bool Withdrawal(decimal sumWithdrawal);
        public abstract bool AnnualPayment();
    }
}
