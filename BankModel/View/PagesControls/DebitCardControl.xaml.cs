using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test.Cards.Realizations;
using Test.Operations.Cards.Realizations;
using Test.Persons.Models;

namespace BankModel.View.PagesControls
{
    /// <summary>
    /// Логика взаимодействия для DebitCardControl.xaml
    /// </summary>
    public partial class DebitCardControl : UserControl
    {
        private DebitCardModel _debitCardModel;
        private IPerson _person;

        public DebitCardControl(DebitCardModel debitCardModel, IPerson person)
        {
            InitializeComponent();

            _person = person;
            _debitCardModel = debitCardModel;
            DataContext = debitCardModel;
        }

        private void OpenCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DebitCard debitCard = new DebitCard(passwordBox.Password, _person, _debitCardModel);
                debitCard.Open();
                MessageBox.Show("Карта оформлена");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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
    }
}
