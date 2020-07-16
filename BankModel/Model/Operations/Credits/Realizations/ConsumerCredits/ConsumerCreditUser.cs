using ConsoleApp1.Operations.Credits.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Operations.Credits.Realizations
{
    public class ConsumerCreditUser : CreditUser, ICreditUser
    {
        private ICreditModel currentModel;
        public override IOperationModel CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                currentModel = (ICreditModel)value;
            }
        }

        public ConsumerCreditUser(decimal initialSum, DateTime termStart, DateTime termEnd, ICreditModel currentCreditModel)
            : base(initialSum, termStart, termEnd, currentCreditModel)
        { }
    }
}
