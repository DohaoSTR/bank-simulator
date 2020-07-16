using BankModel.Model;
using ConsoleApp1.Operations.Credits.Models;
using ConsoleApp1.Operations.Credits.Realizations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Test.Operations.Cards.Models;
using Test.Operations.Cards.Realizations;
using Test.Operations.Deposits.Models;
using Test.Operations.Deposits.Realizations;
using Test.Persons.Models;

namespace Test.Persons.Realizations
{
    public interface IJuridicialPerson : IPerson
    {
        public string NameCompany { get; set; }
        public string AddressCompany { get; set; }
    }

    public class JuridicialPerson : PersonBase, IJuridicialPerson
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
                    SqlCommand command = new SqlCommand("SELECT * FROM [CreditCardJuridicial] WHERE (IdPerson = @IdPerson)", sqlConnection);
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
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [DebitCardJuridicial] WHERE (IdPerson = @IdPerson)", sqlConnection);
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
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [DepositJuridicial] WHERE (Id_Person = @Id_Person)", sqlConnection);
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
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [ConsumerCreditJuridicial] WHERE (Id_Person = @Id_Person)", sqlConnection);
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
                    SqlCommand command1 = new SqlCommand("SELECT * FROM [MortrageJuridicial] WHERE (Id_Person = @Id_Person)", sqlConnection);
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

        private string nameCompany;
        public string NameCompany
        {
            get
            {
                return nameCompany;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Поле - имя компании должно быть заполнено!");
                else
                    nameCompany = value;
            }
        }

        private string addressCompany;
        public string AddressCompany
        {
            get
            {
                return addressCompany;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Поле - адресс компании должно быть заполнено!");
                else
                    addressCompany = value;
            }
        }
        
        protected override bool CheckLogin(string login)
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command2 = new SqlCommand("SELECT Login FROM [JuridicialPerson]", sqlConnection);
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
                SqlCommand command3 = new SqlCommand("SELECT Login FROM [PhysicalPerson]", sqlConnection);
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [JuridicialPerson] (Login, Password, NameCompany, AddressCompany)VALUES(@Login, @Password, @NameCompany, @AddressCompany)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Login", Login);
                sqlCommand.Parameters.AddWithValue("Password", Password);
                sqlCommand.Parameters.AddWithValue("NameCompany", NameCompany);
                sqlCommand.Parameters.AddWithValue("AddressCompany", AddressCompany);
                sqlCommand.ExecuteNonQuery();
                SqlCommand command1 = new SqlCommand("SELECT Id FROM [JuridicialPerson] WHERE (Login = @Login) AND (Password = @Password)", sqlConnection);
                command1.Parameters.AddWithValue("Login", Login);
                command1.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command1.ExecuteReader();
                sqlReader.Read();
                Id = Convert.ToInt32(sqlReader["Id"]);
                sqlReader.Close();
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

        public override bool Authorization()
        {
            using SqlConnection sqlConnection = new SqlConnection(DataBase.ConnectionString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM [JuridicialPerson] WHERE (Login = @Login) AND (Password = @Password)", sqlConnection);
                command.Parameters.AddWithValue("Login", Login);
                command.Parameters.AddWithValue("Password", Password);
                SqlDataReader sqlReader = command.ExecuteReader();
                if (sqlReader.Read() == false)
                {
                    throw new Exception("Данные введены неверно!");
                }
                Id = Convert.ToInt32(sqlReader["Id"]);
                NameCompany = Convert.ToString(sqlReader["NameCompany"]);
                AddressCompany = Convert.ToString(sqlReader["AddressCompany"]);
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

        public JuridicialPerson(string login, string password, string nameCompany, string addressCompany) : base(login, password)
        {
            NameCompany = nameCompany;
            AddressCompany = addressCompany;
        }

        public JuridicialPerson(string login, string password)
            : base(login, password)
        {
            Login = login;
            Password = password;
        }
    }
}
