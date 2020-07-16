using BankModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Test.Cards.Models;

namespace Test.Cards.Realizations
{
    public class DebitCardModel : CardModel, IDebitCardModel
    {
        public DebitCardModel(string name) : base(name)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DebitCardModel] WHERE (Name = @Name)", sqlConnection);
                command.Parameters.AddWithValue("Name", Name);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read() == true)
                {
                    Id = Convert.ToInt32(sqlReader["Id"]);
                }
                AnnualFee = Convert.ToDecimal(sqlReader["AnnualFee"]);
                InitialFee = Convert.ToDecimal(sqlReader["InitialFee"]);
                ValidityYears = Convert.ToInt32(sqlReader["ValidityYears"]);
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

    public interface IDebitCardModel : ICardModel
    {

    }
}
