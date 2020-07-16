using System;
using System.Collections.Generic;
using System.Text;
using Test.Persons.Models;

namespace ConsoleApp1.Operations.Credits.Models
{
    public interface ICreditModel : IOperationModel
    {
        decimal MinInitialSum { get; set; }
        decimal MaxInitialSum { get; set; }
        DateTime MinTermEnd { get; set; }
        DateTime MaxTermEnd { get; set; }
        decimal AnnualRate { get; set; }
        string TypePayment { get; set; }
    }

    public abstract class CreditModel : OperationModel, ICreditModel
    {
        public decimal MinInitialSum { get; set; }
        public decimal MaxInitialSum { get; set; }
        public DateTime MinTermEnd { get; set; }

        private DateTime maxTermEnd;
        public DateTime MaxTermEnd
        {
            get
            {
                return maxTermEnd;
            }
            set
            {
                if (value < MinTermEnd)
                    throw new Exception("Максимальный конец срока кредита установлен не верно");
                else
                    maxTermEnd = value;
            }
        }

        public decimal AnnualRate { get; set; }
        public string TypePayment { get; set; }

        public CreditModel(string name)
            : base(name)
        { }
    }
}
