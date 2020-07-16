using BankModel.Model;
using System;
using System.Data.SqlClient;
using Test.Cards.Models;

namespace Test.Cards.Realizations
{
    public interface ICreditCardModel : ICardModel
    {
        decimal CreditLimit { get; set; }
    }

    public class CreditCardModel : CardModel, ICreditCardModel
    {
        public decimal CreditLimit { get; set; }

        public CreditCardModel(string name) : base(name)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [CreditCardModel] WHERE (Name = @Name)", sqlConnection);
                command.Parameters.AddWithValue("Name", Name);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read() == false)
                {
                    throw new Exception("Данный тариф отсутствует!");
                }
                Id = Convert.ToInt32(sqlReader["Id"]);
                AnnualFee = Convert.ToDecimal(sqlReader["AnnualFee"]);
                InitialFee = Convert.ToDecimal(sqlReader["InitialFee"]);
                ValidityYears = Convert.ToInt32(sqlReader["ValidityYears"]);
                CreditLimit = Convert.ToDecimal(sqlReader["CreditLimit"]);
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
