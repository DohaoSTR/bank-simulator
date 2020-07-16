using BankModel.Model;
using System;
using System.Data.SqlClient;
using Test.Cards.Realizations;
using Test.Operations.Cards.Models;
using Test.Persons.Models;
using Test.Persons.Realizations;

namespace Test.Operations.Cards.Realizations
{
    public interface ICreditCard : ICard
    {
        decimal CurrentCreditLimit { get; set; }
        bool Replenishment(decimal sumReplenishment);
    }

    public class CreditCard : CardBase, ICreditCard
    {
        public decimal CurrentCreditLimit { get; set; }

        public CreditCard(string password, IPerson currentPerson, ICreditCardModel cardModel)
        {
            Password = password;
            CurrentPerson = currentPerson;
            CardModel = cardModel;
            CurrentCreditLimit = cardModel.CreditLimit;
        }

        public CreditCard(int id, string password, IPerson currentPerson)
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
            if(CurrentPerson.GetType().Name == "PhysicalPerson")
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [CreditCardPhysical] (IdPerson, IdCardModel, Password, ValidityStart, ValidityEnd, CurrentCreditLimit)" +
                    "VALUES(@IdPerson, @IdCardModel, @Password, @ValidityStart, @ValidityEnd, @CurrentCreditLimit);SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdPerson", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("IdCardModel", CardModel.Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("ValidityStart", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("ValidityEnd", ValidityEnd);
                sqlCommand.Parameters.AddWithValue("CurrentCreditLimit", CurrentCreditLimit);
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [CreditCardJuridicial] (IdPerson, IdCardModel, Password, ValidityStart, ValidityEnd, CurrentCreditLimit)" +
                    "VALUES(@IdPerson, @IdCardModel, @Password, @ValidityStart, @ValidityEnd, @CurrentCreditLimit);SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("IdPerson", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("IdCardModel", CardModel.Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("ValidityStart", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("ValidityEnd", ValidityEnd);
                sqlCommand.Parameters.AddWithValue("CurrentCreditLimit", CurrentCreditLimit);
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
                SqlCommand command = new SqlCommand("SELECT * FROM [CreditCardPhysical] WHERE (Id = @Id AND Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                CurrentCreditLimit = Convert.ToDecimal(sqlReader["CurrentCreditLimit"]);
                ValidityStart = Convert.ToDateTime(sqlReader["ValidityStart"]);
                int idModel = Convert.ToInt32(sqlReader["IdCardModel"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [CreditCardModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CardModel = new CreditCardModel(name);
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
                SqlCommand command = new SqlCommand("SELECT * FROM [CreditCardJuridicial] WHERE (Id = @Id AND Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read())
                CurrentCreditLimit = Convert.ToDecimal(sqlReader["CurrentCreditLimit"]);
                ValidityStart = Convert.ToDateTime(sqlReader["ValidityStart"]);
                int idModel = Convert.ToInt32(sqlReader["IdCardModel"]);
                sqlReader.Close();

                SqlCommand command1 = new SqlCommand("SELECT * FROM [CreditCardModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CardModel = new CreditCardModel(name);
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
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [CreditCardPhysical] WHERE Id=@Id AND Password=@Password", sqlConnection);
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
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [CreditCardJuridicial] WHERE Id=@Id AND Password=@Password", sqlConnection);
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
            if (CurrentCreditLimit < sumWithdrawal)
                throw new Exception("Сумма снятия больше вашего баланса!");
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [CreditCardCardPhysical] SET CurrentCreditLimit=@CurrentCreditLimit WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("MoneyCount", CurrentCreditLimit - sumWithdrawal);
                sqlCommand.ExecuteNonQuery();
                CurrentCreditLimit -= sumWithdrawal;
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
            if (CurrentCreditLimit < sumWithdrawal)
                throw new Exception("Сумма снятия больше вашего баланса!");
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE [CreditCardJuridicial] SET CurrentCreditLimit=@CurrentCreditLimit WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("CurrentCreditLimit", CurrentCreditLimit - sumWithdrawal);
                sqlCommand.ExecuteNonQuery();
                CurrentCreditLimit -= sumWithdrawal;
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
                SqlCommand sqlCommand = new SqlCommand("UPDATE [CreditCardPhysical] SET CurrentCreditLimit=@CurrentCreditLimit WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("CurrentCreditLimit", CurrentCreditLimit + sumReplenishment);
                sqlCommand.ExecuteNonQuery();
                CurrentCreditLimit += sumReplenishment;
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
                SqlCommand sqlCommand = new SqlCommand("UPDATE [CreditCardJuridicial] SET CurrentCreditLimit=@CurrentCreditLimit WHERE Id=@Id AND Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", Id);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("CurrentCreditLimit", CurrentCreditLimit + sumReplenishment);
                sqlCommand.ExecuteNonQuery();
                CurrentCreditLimit += sumReplenishment;
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
