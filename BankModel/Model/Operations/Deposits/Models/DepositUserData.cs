using ConsoleApp1.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Operations.Deposits.Models
{
    public interface IDepositUser : IOperationUser
    {
        decimal CurrentSum { get; set; }
        decimal InitialSum { get; set; }
        DateTime TermStart { get; set; }
        DateTime TermEnd { get; set; }
    }
    
    public class DepositUserData : OperationUser, IDepositUser
    {
        public decimal CurrentSum { get; set; }

        private decimal initialSum;
        public decimal InitialSum
        {
            get
            {
                return initialSum;
            }
            set
            {
                if (value > currentModel.MaxInitialSum || value < currentModel.MinInitialSum)
                    throw new Exception("Сумма кредита установлена неверно");
                else
                    initialSum = value;
            }
        }

        public DateTime TermStart { get; set; }

        private DateTime termEnd;
        public DateTime TermEnd
        {
            get
            {
                return termEnd;
            }
            set
            {
                if (value < currentModel.MinTermEnd || value > currentModel.MaxTermEnd)
                    throw new Exception("Конец срока кредита установлен не верно");
                else
                    termEnd = value;
            }
        }

        private IDepositModel currentModel;
        public override IOperationModel CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                currentModel = (IDepositModel)value;
            }
        }

        public DepositUserData(IDepositModel currentModel, decimal initialSum, DateTime termStart, DateTime termEnd)
            : base(currentModel)
        {
            CurrentModel = currentModel;
            InitialSum = initialSum;
            TermStart = termStart;
            TermEnd = termEnd;
        }
    }
}
