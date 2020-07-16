using ConsoleApp1.Operations.Credits.Models;
using ConsoleApp1.Operations.Credits.Realizations;
using System;
using System.Collections.Generic;
using Test.Operations.Cards.Models;
using Test.Operations.Cards.Realizations;
using Test.Operations.Deposits.Models;
using Test.Operations.Deposits.Realizations;

namespace Test.Persons.Models
{
    abstract public class PersonBase : IPerson
    {
        public abstract List<CreditCard> CreditCardsList { get; }
        public abstract List<DebitCard> DebitCardsList { get; }
        public abstract List<Deposit> DepositList { get; }
        public abstract List<ConsumerCredit> ConsumerCreditList { get; }
        public abstract List<Mortrage> MortrageList { get; }

        public int Id { get; set; }
        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (value.Length < 3)
                    throw new Exception("Логин должен быть длинной больше трех символов!");
                else
                    login = value;
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value.Length < 5)
                    throw new Exception("Пароль должен быть длинной больше пяти символов!");
                else
                    password = value;
            }
        }

        protected abstract bool CheckLogin(string login);

        public abstract bool Registration();

        public abstract bool Authorization();

        public PersonBase(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
