using BankModel.Model;
using ConsoleApp1.Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Test.Operations.Deposits.Models
{
    public interface IDepositModel : IOperationModel
    {
        decimal MinInitialSum { get; set; }
        decimal MaxInitialSum { get; set; }
        DateTime MinTermEnd { get; set; }
        DateTime MaxTermEnd { get; set; }
        decimal AnnualRate { get; set; }
        string Capitalization { get; set; }
    }

    public class DepositModelData : OperationModel, IDepositModel
    {
        private decimal minInitialSum;
        public decimal MinInitialSum
        {
            get
            {
                return minInitialSum;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Минимальная сумма кредита установлена неверно");
                else
                    minInitialSum = value;
            }
        }

        private decimal maxInitialSum;
        public decimal MaxInitialSum
        {
            get
            {
                return maxInitialSum;
            }
            set
            {
                if (value < 0 || value < MinInitialSum)
                    throw new Exception("Максимальная сумма кредита установлена неверно");
                else
                    maxInitialSum = value;
            }
        }

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

        public string Capitalization { get; set; }

        public DepositModelData(string name) 
            : base(name)
        {
            Name = name;

            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DepositModel] WHERE (Name = @Name)", sqlConnection);
                command.Parameters.AddWithValue("Name", Name);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read() == false)
                {
                    throw new Exception("Данный тариф отсутствует!");
                }
                Id = Convert.ToInt32(sqlReader["Id"]);
                MinInitialSum = Convert.ToDecimal(sqlReader["MinInitialSum"]);
                MaxInitialSum = Convert.ToDecimal(sqlReader["MaxInitialSum"]);
                MinTermEnd = Convert.ToDateTime(sqlReader["MinTermEnd"]);
                MaxTermEnd = Convert.ToDateTime(sqlReader["MaxTermEnd"]);
                AnnualRate = Convert.ToDecimal(sqlReader["AnnualRate"]);
                Capitalization = Convert.ToString(sqlReader["Capitalization"]);
                if (Capitalization == "true")
                    Capitalization = "Вклад с капитализацией";
                else
                    Capitalization = "Вклад без капитализации";
                sqlReader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Работа базы данных нарушена", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
