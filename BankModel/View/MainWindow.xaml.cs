using BankModel.View.PagesControls;
using BankModel.View.Resources;
using BankModel.View.Resources.Menu;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Test.Persons.Models;

namespace BankModel.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonalAccountControl _personalAccountControl;
        private IPerson _person;

        public MainWindow(IPerson person)
        {
            InitializeComponent();

            _person = person;

            var menuCredits = new List<SubItem>();
            menuCredits.Add(new SubItem("Кредит на любые цели"));
            menuCredits.Add(new SubItem("Кредит для молодежи"));
            menuCredits.Add(new SubItem("Кредит для пенсионеров"));
            menuCredits.Add(new SubItem("Кредит «Стандартный»"));
            var item0 = new ItemMenu("Кредиты", menuCredits, PackIconKind.Money);

            var menuMortrage = new List<SubItem>();
            menuMortrage.Add(new SubItem("Ипотека на готовое жилье"));
            menuMortrage.Add(new SubItem("Ипотека на новостройки"));
            menuMortrage.Add(new SubItem("Ипотека без взноса"));
            var item1 = new ItemMenu("Ипотека", menuMortrage, PackIconKind.Home);

            var menuCreditCards = new List<SubItem>();
            menuCreditCards.Add(new SubItem("Премиальная кредитная карта"));
            menuCreditCards.Add(new SubItem("Золотая кредитная карта"));
            menuCreditCards.Add(new SubItem("Цифровая кредитная карта"));
            var item2 = new ItemMenu("Кредитные карты", menuCreditCards, PackIconKind.CreditCard);

            var menuDebitCards = new List<SubItem>();
            menuDebitCards.Add(new SubItem("Молодежная карта"));
            menuDebitCards.Add(new SubItem("Золотая карта"));
            menuDebitCards.Add(new SubItem("Классическая карта"));
            var item3 = new ItemMenu("Дебетовые карты", menuDebitCards, PackIconKind.CardBulleted);

            var menuDeposits = new List<SubItem>();
            menuDeposits.Add(new SubItem("Вклад «Победная ставка»"));
            menuDeposits.Add(new SubItem("Вклад «Выгодное решение»"));
            menuDeposits.Add(new SubItem("Вклад «Копилка»"));
            var item4 = new ItemMenu("Вклады", menuDeposits, PackIconKind.AttachMoney);

            Menu.Children.Add(new MenuItemControl(item0, this, _person));
            Menu.Children.Add(new MenuItemControl(item1, this, _person));
            Menu.Children.Add(new MenuItemControl(item2, this, _person));
            Menu.Children.Add(new MenuItemControl(item3, this, _person));
            Menu.Children.Add(new MenuItemControl(item4, this, _person));
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Turn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PersonalAccount_Click(object sender, RoutedEventArgs e)
        {
            _personalAccountControl = new PersonalAccountControl(_person);
            PageMenu.Children.Clear();
            PageMenu.Children.Add(_personalAccountControl);
        }
    }
}
