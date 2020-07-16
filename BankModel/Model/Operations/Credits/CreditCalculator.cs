using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ConsoleApp1.Operations.Credits
{
    public static class CreditCalculator
    {
        public static decimal MonthlyPaymentAnnuitetWithInitialFee(decimal initialSum, decimal initialFee, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            try
            {
                return (initialSum - initialFee) * MonthlyRate(annualRate) / (1 - Convert.ToDecimal(Math.Pow((double)(1 + MonthlyRate(annualRate)), -MonthCount(termStart, termEnd))));
            }
            catch(DivideByZeroException)
            {
                throw new Exception("Дата введена неверно!");
            }
        }

        public static decimal AnnuitetSumPaymentWithInitialFee(decimal initialSum, decimal initialFee, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            return MonthlyPaymentAnnuitetWithInitialFee(initialSum, initialFee, annualRate, termStart, termEnd) * MonthCount(termStart, termEnd);
        }

        private static decimal MonthlyRate(decimal annualRate)
        {
            return annualRate / 12;
        }

        public static int MonthCount(DateTime termStart, DateTime termEnd)
        {
            return ((termEnd.Year - termStart.Year) * 12) + termEnd.Month - termStart.Month;
        }

        public static decimal MonthlyPaymentAnnuitet(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            return initialSum * AnnuitetRation(annualRate, termStart, termEnd);
        }

        public static decimal AnnuitetSumPayment(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            return MonthlyPaymentAnnuitet(initialSum, annualRate, termStart, termEnd) * MonthCount(termStart, termEnd);
        }

        private static decimal AnnuitetRation(decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            try
            {
                return MonthlyRate(annualRate) / (1 - Convert.ToDecimal(Math.Pow((double)(1 + MonthlyRate(annualRate)), -MonthCount(termStart, termEnd))));
            }
            catch (DivideByZeroException)
            {
                throw new Exception("Дата введена неверно!");
            }
        }

        public static List<int> DaysCount(DateTime termStart, DateTime termEnd)
        {
            List<int> daysCount = new List<int>();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            for (int i = 0; i <= MonthCount(termStart, termEnd); i++)
            {
                daysCount.Add(DateTime.DaysInMonth(year, month));
                if (month == 12)
                {
                    year++;
                    month = 1;
                }
                else
                    month++;
            }
            return daysCount;
        }

        public static decimal DifferSumPayment(decimal initialSum, decimal annualRate, DateTime termStart, DateTime termEnd)
        {
            List<int> daysCount = DaysCount(termStart, termEnd);
            decimal loanDebt = initialSum / MonthCount(termStart, termEnd);
            decimal allSumPayment = 0;
            decimal restSum = initialSum;
            for (int i = 1; i <= MonthCount(termStart, termEnd); i++)
            {
                decimal rateCharged = restSum * annualRate * daysCount[i - 1] / 365;
                allSumPayment += rateCharged + loanDebt;
                restSum -= loanDebt;
            }
            return allSumPayment;
        }
    }
}
