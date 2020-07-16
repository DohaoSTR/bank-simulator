using BankModel.Model;
using ConsoleApp1.Operations.Credits.Models;
using ConsoleApp1.Operations.Credits.Realizations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Test.Operations.Cards.Models;
using Test.Operations.Cards.Realizations;
using Test.Operations.Deposits.Models;
using Test.Operations.Deposits.Realizations;
using Test.Persons.Models;

namespace Test.Persons.Realizations
{
    public interface IPhysicalPerson : IPerson
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class PhysicalPerson : PersonBase, IPhysicalPerson
    {
        public override List<CreditCard> CreditCardsList
        {
            get
            {
                List<CreditCard> cardsList = new List<CreditCard>();
                using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
                try
                {
                    sqlConnection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM [CreditCardPhysical] WHERE (IdPerson = @IdPerson)", sqlConnection);
                    command.Parameters.AddWithValue("IdPerson", Id);
                    SqlDataReader sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                        cardsList.Add(new CreditCard(Convert.ToInt32(sqlReader["Id"]), Convert.ToString(sqlReader["Password"]), this));
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
                return cardsList;
            }
        }
        public override List<DebitCard> DebitCardsList
        {
            get
            {
                List<DebitCard> cardsList = new List<DebitCard>();
                using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
                try
                {
                    sqlConnection.Open();
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [DebitCardPhysical] WHERE (IdPerson = @IdPerson)", sqlConnection);
                    command1.Parameters.AddWithValue("IdPerson", Id);
                    SqlDataReader sqlReader1 = command1.ExecuteReader();
                    while (sqlReader1.Read())
                        cardsList.Add(new DebitCard(Convert.ToInt32(sqlReader1["Id"]), Convert.ToString(sqlReader1["Password"]), this));
                    sqlReader1.Close();
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
                return cardsList;
            }
        }
        public override List<Deposit> DepositList
        {
            get
            {
                List<Deposit> depositList = new List<Deposit>();
                using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
                try
                {
                    sqlConnection.Open();
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [DepositPhysical] WHERE (Id_Person = @Id_Person)", sqlConnection);
                    command1.Parameters.AddWithValue("Id_Person", Id);
                    SqlDataReader sqlReader1 = command1.ExecuteReader();
                    while (sqlReader1.Read())
                        depositList.Add(new Deposit(Convert.ToInt32(sqlReader1["Id"]), this));
                    sqlReader1.Close();
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
                return depositList;
            }
        }
        public override List<ConsumerCredit> ConsumerCreditList
        {
            get
            {
                List<ConsumerCredit> consumerCreditList = new List<ConsumerCredit>();
                using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
                try
                {
                    sqlConnection.Open();
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [ConsumerCreditPhysical] WHERE (Id_Person = @Id_Person)", sqlConnection);
                    command1.Parameters.AddWithValue("Id_Person", Id);
                    SqlDataReader sqlReader1 = command1.ExecuteReader();
                    while (sqlReader1.Read())
                        consumerCreditList.Add(new ConsumerCredit(Convert.ToInt32(sqlReader1["Id"]), this));
                    sqlReader1.Close();
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
                return consumerCreditList;
            }
        }
        public override List<Mortrage> MortrageList
        {
            get
            {
                List<Mortrage> consumerCreditList = new List<Mortrage>();
                using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
                try
                {
                    sqlConnection.Open();
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [MortragePhysical] WHERE (Id_Person = @Id_Person)", sqlConnection);
                    command1.Parameters.AddWithValue("Id_Person", Id);
                    SqlDataReader sqlReader1 = command1.ExecuteReader();
                    while (sqlReader1.Read())
                        consumerCreditList.Add(new Mortrage(Convert.ToInt32(sqlReader1["Id"]), this));
                    sqlReader1.Close();
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
                return consumerCreditList;
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Поле - имя должно быть заполнено!");
                else
                    name = value;
            }
        }
        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Поле - фамилия должно быть заполнено!");
                else
                    surname = value;
            }
        }

        protected override bool CheckLogin(string login)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command2 = new SqlCommand("SELECT Login FROM [PhysicalPerson]", sqlConnection);
                command2.Parameters.AddWithValue("Login", login);
                SqlDataReader checkLogin = command2.ExecuteReader();
                while (checkLogin.Read() == true)
                    if (Convert.ToString(checkLogin["Login"]) == login)
                    {
                        checkLogin.Close();
                        sqlConnection.Close();
                        return true;
                    }
                checkLogin.Close();
                SqlCommand command3 = new SqlCommand("SELECT Login FROM [JuridicialPerson]", sqlConnection);
                command3.Parameters.AddWithValue("Login", login);
                SqlDataReader checkLogin1 = command3.ExecuteReader();
                while (checkLogin1.Read() == true)
                    if (Convert.ToString(checkLogin1["Login"]) == login)
                    {
                        checkLogin1.Close();
                        sqlConnection.Close();
                        return true;
                    }
                checkLogin1.Close();
                return false;
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

        public override bool Authorization()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [PhysicalPerson] WHERE (Login = @Login) AND (Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Login", Login);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read() == false)
                {
                    throw new Exception("Данные введены неверно!");
                }    
                Name = Convert.ToString(sqlReader["Name"]);
                Surname = Convert.ToString(sqlReader["Surname"]);
                Id = Convert.ToInt32(sqlReader["Id"]);
                sqlReader.Close();
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

        public override bool Registration()
        {
            if (CheckLogin(Login) == true)
            {
                throw new Exception("Такой логин уже существует!");
            }    
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [PhysicalPerson] (Login, Password, Name, Surname)VALUES(@Login, @Password, @Name, @Surname)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Login", Login);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("Name", Name);
                sqlCommand.Parameters.AddWithValue("Surname", Surname);
                sqlCommand.ExecuteNonQuery();
                SqlCommand command1 = new SqlCommand("SELECT Id FROM [PhysicalPerson] WHERE (Login = @Login) AND (Password = @Password)", sqlConnection);
                command1.Parameters.AddWithValue("Login", Login);
                command1.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command1.ExecuteReader();
                sqlReader.Read();
                Id = Convert.ToInt32(sqlReader["Id"]);
                sqlReader.Close();
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

        public PhysicalPerson(string login, string password, string name, string surname) 
            : base(login, password)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
        }

        public PhysicalPerson(string login, string password)
            : base(login, password)
        {
            Login = login;
            Password = password;
        }
    }
}
