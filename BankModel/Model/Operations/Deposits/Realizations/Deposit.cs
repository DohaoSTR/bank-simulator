using BankModel.Model;
using ConsoleApp1.Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using Test.Operations.Deposits.Models;
using Test.Persons.Models;
using Test.Persons.Realizations;

namespace Test.Operations.Deposits.Realizations
{
    public class Deposit : DepositBase, IDeposit
    {
        private IDepositModel currentModel;
        public override IOperationModel CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                currentModel = (IDepositModel)value;
            }
        }

        private IDepositUser currentUser;
        public override IOperationUser CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = (IDepositUser)value;
            }
        }

        public Deposit(IDepositUser currentUser, IDepositModel currentModel, IPerson currentPerson)
           : base(currentModel, currentUser, currentPerson)
        {
            CurrentPerson = currentPerson;
            CurrentUser = currentUser;
            CurrentModel = currentModel;
        }

        public Deposit(int id, IPerson currentPerson)
            : base(null, null, currentPerson)
        {
            Id = id;
            CurrentPerson = currentPerson;
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                FillForPhysical(id);
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                FillForJuridicial(id);
            }
        }

        private bool FillForPhysical(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DepositPhysical] WHERE (Id = @Id)", sqlConnection);
                command.Parameters.AddWithValue("Id", id);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                int idModel = Convert.ToInt32(sqlReader["Id_Model"]);
                decimal currentSum = Convert.ToDecimal(sqlReader["CurrentSum"]);
                DateTime termEnd = Convert.ToDateTime(sqlReader["TermEnd"]);
                DateTime termStart = Convert.ToDateTime(sqlReader["TermStart"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [DepositModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CurrentModel = new DepositModelData(name);
                CurrentUser = new DepositUserData((IDepositModel)CurrentModel, currentSum, termStart, termEnd);
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

        private bool FillForJuridicial(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [DepositJuridicial] WHERE (Id = @Id)", sqlConnection);
                command.Parameters.AddWithValue("Id", id);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();   
                int idModel = Convert.ToInt32(sqlReader["Id_Model"]);
                decimal currentSum = Convert.ToDecimal(sqlReader["CurrentSum"]);
                DateTime termEnd = Convert.ToDateTime(sqlReader["TermEnd"]);
                DateTime termStart = Convert.ToDateTime(sqlReader["TermStart"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [DepositModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CurrentModel = new DepositModelData(name);
                CurrentUser = new DepositUserData((IDepositModel)CurrentModel, currentSum, termStart, termEnd);
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

        public override bool Close(int id)
        {
            if (CurrentPerson.GetType().Name == "PhysicalPerson")
            {
                return CloseForPhysical(id);
            }
            else if (CurrentPerson.GetType().Name == "JuridicialPerson")
            {
                return CloseForJuridicial(id);
            }
            return false;
        }

        private bool CloseForPhysical(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [DepositPhysical] WHERE Id=@Id)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
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

        private bool CloseForJuridicial(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [DepositJuridicial] WHERE Id=@Id)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
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

        private bool OpenForPhysical()
        {
            var currentPerson = (IPhysicalPerson)CurrentPerson;
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [DepositPhysical] (Id_Person, Id_Model, CurrentSum, InitialSum, TermStart, TermEnd)" +
                    "VALUES(@Id_Person, @Id_Model, @CurrentSum, @InitialSum, @TermStart, @TermEnd); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id_Person", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("Id_Model", currentModel.Id);
                sqlCommand.Parameters.AddWithValue("InitialSum", currentUser.InitialSum);
                sqlCommand.Parameters.AddWithValue("CurrentSum", currentUser.InitialSum);
                sqlCommand.Parameters.AddWithValue("TermStart", currentUser.TermStart);
                sqlCommand.Parameters.AddWithValue("TermEnd", currentUser.TermEnd);
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [DepositJuridicial] (Id_Person, Id_Model, CurrentSum, InitialSum, TermStart, TermEnd)" +
                    "VALUES(@Id_Person, @Id_Model, @CurrentSum, @InitialSum, @TermStart, @TermEnd); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id_Person", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("Id_Model", currentModel.Id);
                sqlCommand.Parameters.AddWithValue("InitialSum", currentUser.InitialSum);
                sqlCommand.Parameters.AddWithValue("CurrentSum", currentUser.InitialSum);
                sqlCommand.Parameters.AddWithValue("TermStart", currentUser.TermStart);
                sqlCommand.Parameters.AddWithValue("TermEnd", currentUser.TermEnd);
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
    }
    public interface IDeposit : IOperation
    {
    }
}
