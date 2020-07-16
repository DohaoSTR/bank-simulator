using BankModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Test.Cards.Realizations;
using Test.Operations.Cards.Models;
using Test.Persons.Models;
using Test.Persons.Realizations;

namespace Test.Operations.Cards.Realizations
{
    public interface IDebitCard : ICard
    {
        decimal MoneyCount { get; set; }
        bool Replenishment(decimal sumReplenishment);
    }

    public class DebitCard : CardBase, IDebitCard
    {
        public decimal MoneyCount { get; set; }

        public DebitCard(string password, IPerson currentPerson, IDebitCardModel cardModel)
        {
            Password = password;
            CurrentPerson = currentPerson;
            CardModel = cardModel;
        }

        public DebitCard(int id, string password, IPerson currentPerson)
        {
            Id = id;
            Password = password;
            CurrentPerson = currentPerson;

            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                FillForPhysical();
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                FillForJuridicial();
            }
        }

        public override bool Open()
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return OpenForPhysical();
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return OpenForJuridicial();
            }
            return false;
        }

        public override bool Close()
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return CloseForPhysical();
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return CloseForJuridicial();
            }
            return false;
        }

        public override bool Withdrawal(decimal sumWithdrawal)
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return WithdrawalForPhysical(sumWithdrawal);
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return WithdrawalForJuridicial(sumWithdrawal);
            }
            return false;
        }

        public override bool AnnualPayment()
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return WithdrawalForPhysical(CardModel.AnnualFee);
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return WithdrawalForJuridicial(CardModel.AnnualFee);
            }
            return false;
        }

        public bool Replenishment(decimal sumReplenishment)
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return ReplenishmentForPhysical(sumReplenishment);
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return ReplenishmentForJuridicial(sumReplenishment);
            }
            return false;
        }

        private bool OpenForPhysical()
        {
            var currentPerson = (IPhysicalPerson)CurrentPerson;
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [DebitCardPhysical] (IdPerson, IdCardModel, Password, ValidityStart, ValidityEnd, MoneyCount)" +
                    "VALUES(@IdPerson, @IdCardModel, @Password, @ValidityStart, @ValidityEnd, @MoneyCount); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdPerson", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("IdCardModel", CardModel.Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("ValidityStart", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("ValidityEnd", ValidityEnd);
                sqlCommand.Parameters.AddWithValue("MoneyCount", 0);
                Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
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

        private bool OpenForJuridicial()
        {
            var currentPerson = (IJuridicialPerson)CurrentPerson;
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [DebitCardJuridicial] (IdPerson, IdCardModel, Password, ValidityStart, ValidityEnd, MoneyCount)" +
                    "VALUES(@IdPerson, @IdCardModel, @Password, @ValidityStart, @ValidityEnd, @MoneyCount); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdPerson", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("IdCardModel", CardModel.Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("ValidityStart", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("ValidityEnd", ValidityEnd);
                sqlCommand.Parameters.AddWithValue("MoneyCount", 0);
                Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return true;
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

        private bool FillForPhysical()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DebitCardPhysical] WHERE (Id = @Id AND Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                MoneyCount = Convert.ToDecimal(sqlReader["MoneyCount"]);
                ValidityStart = Convert.ToDateTime(sqlReader["ValidityStart"]);
                int idModel = Convert.ToInt32(sqlReader["IdCardModel"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [DebitCardModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CardModel = new DebitCardModel(name);
                sqlReader1.Close();
                sqlConnection.Close();
                return true;
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

        private bool FillForJuridicial()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DebitCardJuridicial] WHERE (Id = @Id AND Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                MoneyCount = Convert.ToDecimal(sqlReader["MoneyCount"]);
                ValidityStart = Convert.ToDateTime(sqlReader["ValidityStart"]);
                int idModel = Convert.ToInt32(sqlReader["IdCardModel"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [DebitCardModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CardModel = new DebitCardModel(name);
                sqlReader1.Close();
                sqlConnection.Close();
                return true;
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

        private bool CloseForPhysical()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [DebitCardPhysical] WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.ExecuteNonQuery();
                return true;
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

        private bool CloseForJuridicial()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [DebitCardJuridicial] WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.ExecuteNonQuery();
                return true;
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

        private bool WithdrawalForPhysical(decimal sumWithdrawal)
        {
            if (MoneyCount < sumWithdrawal)
                throw new Exception("Сумма снятия больше вашего баланса!");
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [DebitCardPhysical] SET MoneyCount=@MoneyCount WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("MoneyCount", MoneyCount - sumWithdrawal);
                sqlCommand.ExecuteNonQuery();
                MoneyCount -= sumWithdrawal;
                return true;
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

        private bool WithdrawalForJuridicial(decimal sumWithdrawal)
        {
            if (MoneyCount < sumWithdrawal)
                throw new Exception("Сумма снятия больше вашего баланса!");
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [DebitCardJuridicial] SET MoneyCount=@MoneyCount WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("MoneyCount", MoneyCount - sumWithdrawal);
                sqlCommand.ExecuteNonQuery();
                MoneyCount -= sumWithdrawal;
                return true;
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

        private bool ReplenishmentForPhysical(decimal sumReplenishment)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [DebitCardPhysical] SET MoneyCount=@MoneyCount WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("MoneyCount", MoneyCount + sumReplenishment);
                sqlCommand.ExecuteNonQuery();
                MoneyCount += sumReplenishment;
                return true;
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

        private bool ReplenishmentForJuridicial(decimal sumReplenishment)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [DebitCardJuridicial] SET MoneyCount=@MoneyCount WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("MoneyCount", MoneyCount + sumReplenishment);
                sqlCommand.ExecuteNonQuery();
                MoneyCount += sumReplenishment;
                return true;
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
