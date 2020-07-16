using ConsoleApp1.Operations.Credits.Realizations;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Test.Cards.Realizations;
using Test.Operations.Cards.Realizations;
using Test.Operations.Deposits.Models;
using Test.Operations.Deposits.Realizations;
using Test.Persons.Models;
using Test.Persons.Realizations;

namespace BankModel.View.PagesControls
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountControl.xaml
    /// </summary>
    public partial class PersonalAccountControl : UserControl
    {
        private IPerson _person;
        private ObservableCollection<DebitCardCollection> _debitCards = new ObservableCollection<DebitCardCollection>();
        private ObservableCollection<CreditCardCollection> _creditCards = new ObservableCollection<CreditCardCollection>();
        private ObservableCollection<MortrageCollection> _mortrages = new ObservableCollection<MortrageCollection>();
        private ObservableCollection<ConsumerCreditCollection> _consumerCredits = new ObservableCollection<ConsumerCreditCollection>();
        private ObservableCollection<DepositCollection> _deposits = new ObservableCollection<DepositCollection>();


        public PersonalAccountControl(IPerson person)
        {
            InitializeComponent();

            _person = person;

            foreach(var el in person.DebitCardsList)
            {
                _debitCards.Add(new DebitCardCollection((DebitCardModel)el.CardModel, el));
            }
            foreach (var el in person.CreditCardsList)
            {
                _creditCards.Add(new CreditCardCollection((CreditCardModel)el.CardModel, el));
            }
            foreach (var el in person.MortrageList)
            {
                _mortrages.Add(new MortrageCollection((MortrageModel)el.CurrentModel, (MortrageUser)el.CurrentUser, el));
            }
            foreach (var el in person.ConsumerCreditList)
            {
                _consumerCredits.Add(new ConsumerCreditCollection((ConsumerCreditModel)el.CurrentModel, (ConsumerCreditUser)el.CurrentUser, el));
            }
            foreach (var el in person.DepositList)
            {
                _deposits.Add(new DepositCollection((DepositModelData)el.CurrentModel, (DepositUserData)el.CurrentUser, el));
            }
            ListDebitCards.ItemsSource = _debitCards;
            ListCreditCards.ItemsSource = _creditCards;
            ListMortrage.ItemsSource = _mortrages;
            ListConsumerCredits.ItemsSource = _consumerCredits;
            ListDeposits.ItemsSource = _deposits;
        }

        private void WithdrawalDebit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListDebitCards.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                DebitCardCollection mortrage = ListDebitCards.SelectedItem as DebitCardCollection;
                DebitCard mortrage1 = new DebitCard(mortrage.CreditCard.Id, mortrage.CreditCard.Password, _person);
                mortrage1.Withdrawal(Convert.ToDecimal(withdrawalSumDebit.Value));

                int index = _debitCards.IndexOf(ListDebitCards.SelectedItem as DebitCardCollection);
                if (index >= 0)
                    _debitCards[index] = new DebitCardCollection(mortrage.CreditCardModel, mortrage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void WithdrawalCredit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListCreditCards.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                CreditCardCollection mortrage = ListCreditCards.SelectedItem as CreditCardCollection;
                CreditCard mortrage1 = new CreditCard(mortrage.CreditCard.Id, mortrage.CreditCard.Password, _person);
                mortrage1.Withdrawal(Convert.ToDecimal(withdrawalSumCredit.Value));

                int index = _creditCards.IndexOf(ListCreditCards.SelectedItem as CreditCardCollection);
                if (index >= 0)
                    _creditCards[index] = new CreditCardCollection(mortrage.CreditCardModel, mortrage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ReplenishmentDebit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListDebitCards.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                DebitCardCollection mortrage = ListDebitCards.SelectedItem as DebitCardCollection;
                DebitCard mortrage1 = new DebitCard(mortrage.CreditCard.Id, mortrage.CreditCard.Password, _person);
                mortrage1.Replenishment(Convert.ToDecimal(replenSumDebit.Value));

                int index = _debitCards.IndexOf(ListDebitCards.SelectedItem as DebitCardCollection);
                if (index >= 0)
                    _debitCards[index] = new DebitCardCollection(mortrage.CreditCardModel, mortrage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ReplenishmentСredit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListCreditCards.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                CreditCardCollection mortrage = ListCreditCards.SelectedItem as CreditCardCollection;
                CreditCard mortrage1 = new CreditCard(mortrage.CreditCard.Id, mortrage.CreditCard.Password, _person);
                mortrage1.Replenishment(Convert.ToDecimal(replenSumCredit.Value));
                int index = _creditCards.IndexOf(ListCreditCards.SelectedItem as CreditCardCollection);
                if (index >= 0)
                    _creditCards[index] = new CreditCardCollection(mortrage.CreditCardModel, mortrage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CloseCredit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListConsumerCredits.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                ConsumerCreditCollection mortrage = ListConsumerCredits.SelectedItem as ConsumerCreditCollection;
                ConsumerCredit mortrage1 = new ConsumerCredit(mortrage.Id, _person);
                mortrage1.Close(mortrage1.Id);
                _consumerCredits.Remove(ListConsumerCredits.SelectedItem as ConsumerCreditCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CloseMortrage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (ListMortrage.SelectedItem == null)
                    throw new Exception("Выберете строку!");
                MortrageCollection mortrage = ListMortrage.SelectedItem as MortrageCollection;
                Mortrage mortrage1 = new Mortrage(mortrage.Id, _person);
                mortrage1.Close(mortrage1.Id);
                _mortrages.Remove(ListMortrage.SelectedItem as MortrageCollection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }

    public class CreditCardCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentCreditLimit { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public decimal AnnualFee { get; set; }
        public CreditCardModel CreditCardModel { get; set; }
        public CreditCard CreditCard { get; set; }

        public CreditCardCollection(CreditCardModel mortrageModel, CreditCard debitCard)
        {
            CreditCardModel = mortrageModel;
            CreditCard = debitCard;
            Id = debitCard.Id;
            Name = mortrageModel.Name;
            CurrentCreditLimit = Convert.ToDecimal(debitCard.CurrentCreditLimit.ToString("N1"));
            ValidityStart = debitCard.ValidityStart;
            ValidityEnd = debitCard.ValidityEnd;
            AnnualFee = Convert.ToDecimal(mortrageModel.AnnualFee.ToString("N1"));
        }
    }

    public class DebitCardCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MoneyCount { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public decimal AnnualFee { get; set; }
        public DebitCardModel CreditCardModel { get; set; }
        public DebitCard CreditCard { get; set; }

        public DebitCardCollection(DebitCardModel mortrageModel, DebitCard debitCard)
        {
            CreditCardModel = mortrageModel;
            CreditCard = debitCard;
            Id = debitCard.Id;
            Name = mortrageModel.Name;
            MoneyCount = Convert.ToDecimal(debitCard.MoneyCount.ToString("N1"));
            ValidityStart = debitCard.ValidityStart;
            ValidityEnd = debitCard.ValidityEnd;
            AnnualFee = Convert.ToDecimal(mortrageModel.AnnualFee.ToString("N1"));
        }
    }

    public class MortrageCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentSum { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
        public decimal AnnualRate { get; set; }

        public MortrageCollection(MortrageModel mortrageModel, MortrageUser mortrageUser, Mortrage mortrage)
        {
            Id = mortrage.Id;
            Name = mortrageModel.Name;
            CurrentSum = Convert.ToDecimal(mortrageUser.InitialSum.ToString("N1"));
            TermStart = mortrageUser.TermStart;
            TermEnd = mortrageUser.TermEnd;
            AnnualRate = Convert.ToDecimal((mortrageModel.AnnualRate * 100).ToString("N1"));
        }
    }

    public class ConsumerCreditCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentSum { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
        public decimal AnnualRate { get; set; }

        public ConsumerCreditCollection(ConsumerCreditModel mortrageModel, ConsumerCreditUser mortrageUser, ConsumerCredit mortrage)
        {
            Id = mortrage.Id;
            Name = mortrageModel.Name;
            CurrentSum = Convert.ToDecimal(mortrageUser.InitialSum.ToString("N1"));
            TermStart = mortrageUser.TermStart;
            TermEnd = mortrageUser.TermEnd;
            AnnualRate = Convert.ToDecimal((mortrageModel.AnnualRate * 100).ToString("N1"));
        }
    }

    public class DepositCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentSum { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; set; }
        public decimal AnnualRate { get; set; }

        public DepositCollection(DepositModelData mortrageModel, DepositUserData mortrageUser, Deposit mortrage)
        {
            Id = mortrage.Id;
            Name = mortrageModel.Name;
            CurrentSum = Convert.ToDecimal(mortrageUser.InitialSum.ToString("N1"));
            TermStart = mortrageUser.TermStart;
            TermEnd = mortrageUser.TermEnd;
            AnnualRate = Convert.ToDecimal((mortrageModel.AnnualRate * 100).ToString("N1"));
        }
    }
}
