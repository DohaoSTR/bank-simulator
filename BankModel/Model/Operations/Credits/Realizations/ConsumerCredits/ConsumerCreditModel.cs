using BankModel.Model;
using ConsoleApp1.Operations.Credits.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp1.Operations.Credits.Realizations
{
    public class ConsumerCreditModel : CreditModel, ICreditModel
    {
        public ConsumerCreditModel(string name)
            : base(name)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [ConsumerCreditModel] WHERE (Name = @Name)", sqlConnection);
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
                TypePayment = Convert.ToString(sqlReader["TypePayment"]);
                sqlReader.Close();
                sqlConnection.Close();
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
