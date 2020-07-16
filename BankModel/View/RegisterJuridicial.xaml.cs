using System;
using System.Windows;
using System.Windows.Input;
using Test.Persons.Realizations;

namespace BankModel.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterJuridicial.xaml
    /// </summary>
    public partial class RegisterJuridicial : Window
    {
        private JuridicialPerson _juridicialPerson;
        private LoginWindow _loginWindow;

        public RegisterJuridicial()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loginBox.Text == "Логин" && passwordBox.Password == "Password" && nameCompany.Text == "Имя компании" && addressCompany.Text == "Адресс компании")
                    throw new Exception("Измените начальные данные!");
                _juridicialPerson = new JuridicialPerson(loginBox.Text, passwordBox.Password, nameCompany.Text, addressCompany.Text);
                if (_juridicialPerson.Registration())
                {
                    MessageBox.Show("Регистрация успешна!");
                    _loginWindow = new LoginWindow();
                    _loginWindow.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Turn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "Логин")
                loginBox.Clear();
        }

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "")
                loginBox.Text = "Логин";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "Password")
                passwordBox.Clear();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
                passwordBox.Password = "Password";
        }

        private void NameCompany_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameCompany.Text == "Имя компании")
                nameCompany.Clear();
        }

        private void NameCompany_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameCompany.Text == "")
                nameCompany.Text = "Имя компании";
        }

        private void AddressCompany_GotFocus(object sender, RoutedEventArgs e)
        {
            if (addressCompany.Text == "Адресс компании")
                addressCompany.Clear();
        }

        private void AddressCompany_LostFocus(object sender, RoutedEventArgs e)
        {
            if (addressCompany.Text == "")
                addressCompany.Text = "Адресс компании";
        }
    }
}
