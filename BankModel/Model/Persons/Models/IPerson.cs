using ConsoleApp1.Operations.Credits.Realizations;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Operations.Cards.Realizations;
using Test.Operations.Deposits.Realizations;

namespace Test.Persons.Models
{
    public interface IPerson
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Registration();
        public bool Authorization();
        public List<CreditCard> CreditCardsList { get; }
        public List<DebitCard> DebitCardsList { get; }
        public List<Deposit> DepositList { get; }
        public List<ConsumerCredit> ConsumerCreditList { get; }
        public List<Mortrage> MortrageList { get; }
    }
}
