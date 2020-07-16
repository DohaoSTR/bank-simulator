using BankModel.Model;
using ConsoleApp1.Operations.Credits.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Test.Persons.Models;
using Test.Persons.Realizations;

namespace ConsoleApp1.Operations.Credits.Realizations
{
    public class Mortrage : CreditBase
    {
        private IMortrageModel currentModel;
        public override IOperationModel CurrentModel
        {
            get
            {
                return currentModel;
            }
            set
            {
                currentModel = (IMortrageModel)value;
            }
        }

        private IMortrageUser currentUserData;
        public override IOperationUser CurrentUser
        {
            get
            {
                return currentUserData;
            }
            set
            {
                currentUserData = (IMortrageUser)value;
            }
        }

        public Mortrage(IMortrageModel currentModelData, IMortrageUser currentUserData, IPerson currentPerson)
            : base(currentModelData, currentUserData, currentPerson)
        {
            CurrentUser = currentUserData;
            CurrentModel = currentModelData;
        }

        public Mortrage(int id, IPerson currentPerson)
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
                SqlCommand command = new SqlCommand("SELECT * FROM [MortragePhysical] WHERE (Id = @Id)", sqlConnection);
                command.Parameters.AddWithValue("Id", id);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                int idModel = Convert.ToInt32(sqlReader["Id_Model"]);
                decimal currentSum = Convert.ToDecimal(sqlReader["CurrentSum"]);
                DateTime termEnd = Convert.ToDateTime(sqlReader["TermEnd"]);
                DateTime termStart = Convert.ToDateTime(sqlReader["TermStart"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [MortrageModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CurrentModel = new MortrageModel(name);
                CurrentUser = new MortrageUser(currentSum, termStart, termEnd, (IMortrageModel)CurrentModel, currentSum);
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
                SqlCommand command = new SqlCommand("SELECT * FROM [MortrageJuridicial] WHERE (Id = @Id)", sqlConnection);
                command.Parameters.AddWithValue("Id", id);
                SqlDataReader sqlReader = command.ExecuteReader();
                sqlReader.Read();
                int idModel = Convert.ToInt32(sqlReader["Id_Model"]);
                decimal currentSum = Convert.ToDecimal(sqlReader["CurrentSum"]);
                DateTime termEnd = Convert.ToDateTime(sqlReader["TermEnd"]);
                DateTime termStart = Convert.ToDateTime(sqlReader["TermStart"]);
                sqlReader.Close();
                SqlCommand command1 = new SqlCommand("SELECT * FROM [MortrageModel] WHERE (Id = @Id)", sqlConnection);
                command1.Parameters.AddWithValue("Id", idModel);
                SqlDataReader sqlReader1 = command1.ExecuteReader();
                sqlReader1.Read();
                string name = Convert.ToString(sqlReader1["Name"]);
                CurrentModel = new MortrageModel(name);
                CurrentUser = new MortrageUser(currentSum, termStart, termEnd, (IMortrageModel)CurrentModel, currentSum);
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
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [MortragePhysical] WHERE Id=@Id", sqlConnection);
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
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [MortrageJuridicial] WHERE Id=@Id", sqlConnection);
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [MortragePhysical] (Id_Person, Id_Model, InitialSum, CurrentSum, TermStart, TermEnd, InitialFee)" +
                    "VALUES(@Id_Person, @Id_Model, @InitialSum, @CurrentSum, @TermStart, @TermEnd, @InitialFee); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id_Person", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("Id_Model", currentModel.Id);
                sqlCommand.Parameters.AddWithValue("InitialSum", currentUserData.InitialSum);
                sqlCommand.Parameters.AddWithValue("CurrentSum", currentUserData.InitialSum);
                sqlCommand.Parameters.AddWithValue("TermStart", currentUserData.TermStart);
                sqlCommand.Parameters.AddWithValue("TermEnd", currentUserData.TermEnd);
                sqlCommand.Parameters.AddWithValue("InitialFee", currentUserData.InitialFee);
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [MortrageJuridicial] (Id_Person, Id_Model, InitialSum, CurrentSum, TermStart, TermEnd, InitialFee)" +
                    "VALUES(@Id_Person, @Id_Model, @InitialSum, @CurrentSum, @TermStart, @TermEnd, @InitialFee); SELECT SCOPE_IDENTITY();", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id_Person", currentPerson.Id);
                sqlCommand.Parameters.AddWithValue("Id_Model", currentModel.Id);
                sqlCommand.Parameters.AddWithValue("InitialSum", currentUserData.InitialSum);
                sqlCommand.Parameters.AddWithValue("CurrentSum", currentUserData.InitialSum);
                sqlCommand.Parameters.AddWithValue("TermStart", currentUserData.TermStart);
                sqlCommand.Parameters.AddWithValue("TermEnd", currentUserData.TermEnd);
                sqlCommand.Parameters.AddWithValue("InitialFee", currentUserData.InitialFee);
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
}
