using ConsoleApp1.Operations.Credits.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Operations.Credits.Realizations
{
    public interface IMortrageUser : ICreditUser
    {
        decimal InitialFee { get; set; }
    }

    public class MortrageUser : CreditUser, IMortrageUser
    {
        private IMortrageModel CurrentModelData;
        public decimal InitialFee { get; set; }

        public MortrageUser(decimal initialSum, DateTime termStart, DateTime termEnd, IMortrageModel currentModelData, decimal initialFee)
            : base(initialSum, termStart, termEnd, currentModelData)
        {
            CurrentModelData = currentModelData;
            InitialFee = initialFee;
        }
    }
}
