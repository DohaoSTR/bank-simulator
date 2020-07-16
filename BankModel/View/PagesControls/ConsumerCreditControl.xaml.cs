using ConsoleApp1.Operations.Credits;
using ConsoleApp1.Operations.Credits.Realizations;
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
using Test.Operations.Deposits.Models;
using Test.Persons.Models;

namespace BankModel.View.PagesControls
{
    /// <summary>
    /// Логика взаимодействия для ConsumerCreditControl.xaml
    /// </summary>
    public partial class ConsumerCreditControl : UserControl
    {
        private ConsumerCreditModel _consumerCreditModel;
        private ConsumerCreditUser _consumerCreditUser;
        private IPerson _person;

        public ConsumerCreditControl(ConsumerCreditModel consumerCreditModel, IPerson person)
        {
            InitializeComponent();

            _consumerCreditModel = consumerCreditModel;
            _person = person;
            consumerCreditModel.AnnualRate = Convert.ToDecimal((consumerCreditModel.AnnualRate * 100).ToString("N1"));
            consumerCreditModel.MinInitialSum = Convert.ToDecimal(consumerCreditModel.MinInitialSum.ToString("N1"));
            consumerCreditModel.MaxInitialSum = Convert.ToDecimal(consumerCreditModel.MaxInitialSum.ToString("N1"));
            DataContext = consumerCreditModel;
        }

        private void OpenCredit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePickerCalc.Text == "")
                    throw new Exception("Вы не указали дату окончания кредита!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= _consumerCreditModel.MinTermEnd || Convert.ToDateTime(datePickerCalc.Text) >= _consumerCreditModel.MaxTermEnd)
                    throw new Exception("Дата окончания кредита указана неверно!");
                _consumerCreditUser = new ConsumerCreditUser(Convert.ToDecimal(sliderSum.Value), DateTime.Now, Convert.ToDateTime(datePickerCalc.Text), _consumerCreditModel);
                ConsumerCredit mortrage = new ConsumerCredit(_consumerCreditModel, _consumerCreditUser, _person);
                mortrage.Open();
                MessageBox.Show("Кредит оформлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Calculation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePickerCalc.Text == "")
                    throw new Exception("Вы не указали дату окончания кредита!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= DateTime.Now)
                    throw new Exception("Дата окончания кредита указана неверно!");
                if (_consumerCreditModel.TypePayment == "Annuitet")
                {
                    SumCredit.Content = CreditCalculator.AnnuitetSumPayment(Convert.ToDecimal(sliderSum.Value), _consumerCreditModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                    MonthlyPayment.Content = CreditCalculator.MonthlyPaymentAnnuitet(Convert.ToDecimal(sliderSum.Value), _consumerCreditModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                }
                else
                {
                    SumCredit.Content = CreditCalculator.DifferSumPayment(Convert.ToDecimal(sliderSum.Value), _consumerCreditModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                    MonthlyPayment.Content = CreditCalculator.MonthlyPaymentAnnuitet(Convert.ToDecimal(sliderSum.Value), _consumerCreditModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                    MessageBox.Show("Расчет не точен т.к. при дифференцируемых платежах ежемесячный платеж может отличаться!");
                }
                MessageBox.Show("Расчет произведён!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
