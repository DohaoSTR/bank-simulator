using BankModel.View.PagesControls;
using BankModel.View.Resources.Menu;
using ConsoleApp1.Operations.Credits.Realizations;
using System.Windows;
using System.Windows.Controls;
using Test.Cards.Realizations;
using Test.Operations.Deposits.Models;
using Test.Persons.Models;

namespace BankModel.View.Resources
{
    /// <summary>
    /// Логика взаимодействия для MenuItemControl.xaml
    /// </summary>
    public partial class MenuItemControl : UserControl
    {
        private MainWindow _mainWindow;
        private IPerson _person;

        public MenuItemControl(ItemMenu itemMenu, MainWindow mainWindow, IPerson person)
        {
            InitializeComponent();

            _person = person;
            _mainWindow = mainWindow;
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            DataContext = itemMenu;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            DebitCardModel debitCardModel;
            DebitCardControl debitCardControl;

            CreditCardModel creditCardModel;
            CreditCardControl creditCardControl;

            DepositModelData depositModelData;
            DepositControl depositControl;

            ConsumerCreditModel consumerCreditModel;
            ConsumerCreditControl consumerCreditControl;

            MortrageModel mortrageModel;
            MortrageControl mortrageControl;

            Button button = (Button)sender;
            switch (button.Content)
            {
                case "Молодежная карта":
                    debitCardModel = new DebitCardModel("Молодежная карта");
                    debitCardControl = new DebitCardControl(debitCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(debitCardControl);
                    break;
                case "Золотая карта":
                    debitCardModel = new DebitCardModel("Золотая карта");
                    debitCardControl = new DebitCardControl(debitCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(debitCardControl);
                    break;
                case "Классическая карта":
                    debitCardModel = new DebitCardModel("Классическая карта");
                    debitCardControl = new DebitCardControl(debitCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(debitCardControl);
                    break;
                case "Премиальная кредитная карта":
                    creditCardModel = new CreditCardModel("Премиальная кредитная карта");
                    creditCardControl = new CreditCardControl(creditCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(creditCardControl);
                    break;
                case "Золотая кредитная карта":
                    creditCardModel = new CreditCardModel("Золотая кредитная карта");
                    creditCardControl = new CreditCardControl(creditCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(creditCardControl);
                    break;
                case "Цифровая кредитная карта":
                    creditCardModel = new CreditCardModel("Цифровая кредитная карта");
                    creditCardControl = new CreditCardControl(creditCardModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(creditCardControl);
                    break;
                case "Вклад «Победная ставка»":
                    depositModelData = new DepositModelData("Вклад «Победная ставка»");
                    depositControl = new DepositControl(depositModelData, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(depositControl);
                    break;
                case "Вклад «Выгодное решение»":
                    depositModelData = new DepositModelData("Вклад «Выгодное решение»");
                    depositControl = new DepositControl(depositModelData, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(depositControl);
                    break;
                case "Вклад «Копилка»":
                    depositModelData = new DepositModelData("Вклад «Копилка»");
                    depositControl = new DepositControl(depositModelData, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(depositControl);
                    break;
                case "Кредит на любые цели":
                    consumerCreditModel = new ConsumerCreditModel("Кредит на любые цели");
                    consumerCreditControl = new ConsumerCreditControl(consumerCreditModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(consumerCreditControl);
                    break;
                case "Кредит для молодежи":
                    consumerCreditModel = new ConsumerCreditModel("Кредит для молодежи");
                    consumerCreditControl = new ConsumerCreditControl(consumerCreditModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(consumerCreditControl);
                    break;
                case "Кредит для пенсионеров":
                    consumerCreditModel = new ConsumerCreditModel("Кредит для пенсионеров");
                    consumerCreditControl = new ConsumerCreditControl(consumerCreditModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(consumerCreditControl);
                    break;
                case "Кредит «Стандартный»":
                    consumerCreditModel = new ConsumerCreditModel("Кредит «Стандартный»");
                    consumerCreditControl = new ConsumerCreditControl(consumerCreditModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(consumerCreditControl);
                    break;
                case "Ипотека на готовое жилье":
                    mortrageModel = new MortrageModel("Ипотека на готовое жилье");
                    mortrageControl = new MortrageControl(mortrageModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(mortrageControl);
                    break;
                case "Ипотека на новостройки":
                    mortrageModel = new MortrageModel("Ипотека на новостройки");
                    mortrageControl = new MortrageControl(mortrageModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(mortrageControl);
                    break;
                case "Ипотека без взноса":
                    mortrageModel = new MortrageModel("Ипотека без взноса");
                    mortrageControl = new MortrageControl(mortrageModel, _person);
                    _mainWindow.PageMenu.Children.Clear();
                    _mainWindow.PageMenu.Children.Add(mortrageControl);
                    break;
            }
        }
    }
}
