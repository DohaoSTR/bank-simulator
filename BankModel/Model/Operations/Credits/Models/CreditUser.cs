using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Operations.Credits.Models
{
    public interface ICreditUser : IOperationUser
    {
        decimal CurrentSum { get; set; }
        decimal InitialSum { get; set; }
        DateTime TermStart { get; set; }
        DateTime TermEnd { get; set; }
    }

    public abstract class CreditUser : OperationUser, ICreditUser
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

        public decimal CurrentSum { get; set; }
        public decimal InitialSum { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }

        public CreditUser(decimal initialSum, DateTime termStart, DateTime termEnd, ICreditModel currentModel) 
            : base(currentModel)
        {
            CurrentModel = currentModel;
            InitialSum = initialSum;
            TermStart = termStart;
            TermEnd = termEnd;
        }
    }
}
