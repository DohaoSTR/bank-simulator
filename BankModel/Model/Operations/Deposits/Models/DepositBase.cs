using BankModel.Model.Operations.Deposits.Models;
using ConsoleApp1.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Persons.Models;

namespace Test.Operations.Deposits.Models
{
    abstract public class DepositBase : OperationBase, IDeposit
    {
        public DepositBase(IDepositModel currentModel, IDepositUser currentUser, IPerson currentPerson) : base(currentModel, currentUser, currentPerson)
        {
            CurrentUser = currentUser;
            CurrentModel = currentModel;
        }
    }
}
