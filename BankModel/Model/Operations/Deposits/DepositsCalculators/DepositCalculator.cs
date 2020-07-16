using System;

namespace Test.Operations.Deposits.DepositsCalculators
{
    public static class DepositCalculator
    {
        public static decimal SumInEndTermWithCapitalization(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            decimal sumInEndTerm = initialSum;
            for (int i = 1; i <= ((termEnd.Year - termStart.Year) * 12) + termEnd.Month - termStart.Month; i++)
                sumInEndTerm += sumInEndTerm * annualRate / 12;
            return sumInEndTerm;
        }

        public static decimal SumInEndTermWithoutCapitalization(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            decimal sumInEndTerm = initialSum;
            for (int i = 1; i <= ((termEnd.Year - termStart.Year) * 12) + termEnd.Month - termStart.Month; i++)
                sumInEndTerm += initialSum * annualRate / 12;
            return sumInEndTerm;
        }

        public static decimal SumPercentInEndTermWithCapitalization(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            return SumInEndTermWithCapitalization(initialSum, annualRate, termStart, termEnd) - initialSum;
        }

        public static decimal SumPercentInEndTermWithoutCapitalization(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            return SumInEndTermWithoutCapitalization(initialSum, annualRate, termStart, termEnd) - initialSum;
        }

        public static decimal EffectiveRateWithCapitalization(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            decimal effectiveRate = SumInEndTermWithCapitalization(initialSum, annualRate, termStart, termEnd) / initialSum * (365 / (termEnd - termStart).Days);
            return effectiveRate;
        }
    }
}
