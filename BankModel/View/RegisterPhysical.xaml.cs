using System;
using System.Windows;
using System.Windows.Input;
using Test.Persons.Realizations;

namespace BankModel.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterPhysical.xaml
    /// </summary>
    public partial class RegisterPhysical : Window
    {
        private PhysicalPerson _physicalPerson;
        private LoginWindow _loginWindow;

        public RegisterPhysical()
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
                if (loginBox.Text == "Логин" && passwordBox.Password == "Password" && name.Text == "Имя" && surname.Text == "Фамилия")
                    throw new Exception("Измените начальные данные!");
                _physicalPerson = new PhysicalPerson(loginBox.Text, passwordBox.Password, name.Text, surname.Text);
                if (_physicalPerson.Registration())
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

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (name.Text == "Имя")
                name.Clear();
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
                name.Text = "Имя";
        }

        private void Surname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (surname.Text == "Фамилия")
                surname.Clear();
        }

        private void Surname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (surname.Text == "")
                surname.Text = "Фамилия";
        }
    }
}
