using System;
using System.Windows;
using System.Windows.Input;
using Test.Persons.Realizations;

namespace BankModel.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private PhysicalPerson _physicalPerson;
        private JuridicialPerson _juridicialPerson;
        private MainWindow _mainWindow;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AuthorizationJuridicial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _juridicialPerson = new JuridicialPerson(loginBox.Text, passwordBox.Password);
                if (_juridicialPerson.Authorization())
                {
                    _mainWindow = new MainWindow(_juridicialPerson);
                    _mainWindow.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AuthorizationPhysical_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _physicalPerson = new PhysicalPerson(loginBox.Text, passwordBox.Password);
                if (_physicalPerson.Authorization())
                {
                    _mainWindow = new MainWindow(_physicalPerson);
                    _mainWindow.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void RegForJuridicialWindow_Click(object sender, RoutedEventArgs e)
        {
            RegisterJuridicial regForJuridicialWindow = new RegisterJuridicial();
            regForJuridicialWindow.Show();
            Hide();
        }

        private void RegForPhysicalWindow_Click(object sender, RoutedEventArgs e)
        {
            RegisterPhysical regForPhysicalWindow = new RegisterPhysical();
            regForPhysicalWindow.Show();
            Hide();
        }

        private void Turn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void LoginWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "Логин")
                loginBox.Clear();
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

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "")
                loginBox.Text = "Логин";
        }
    }
}
