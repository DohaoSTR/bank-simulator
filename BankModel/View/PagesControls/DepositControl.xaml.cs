using System;
using System.Windows;
using System.Windows.Controls;
using Test.Operations.Deposits.DepositsCalculators;
using Test.Operations.Deposits.Models;
using Test.Operations.Deposits.Realizations;
using Test.Persons.Models;

namespace BankModel.View.PagesControls
{
    /// <summary>
    /// Логика взаимодействия для DepositControl.xaml
    /// </summary>
    public partial class DepositControl : UserControl
    {
        private DepositModelData _depositModelData;
        private DepositUserData _depositUserData;
        private IPerson _person;

        public DepositControl(DepositModelData depositModelData, IPerson person)
        {
            InitializeComponent();

            _depositModelData = depositModelData;
            _person = person;
            depositModelData.AnnualRate = Convert.ToDecimal((depositModelData.AnnualRate * 100).ToString("N1"));
            depositModelData.MinInitialSum = Convert.ToDecimal(depositModelData.MinInitialSum.ToString("N1"));
            depositModelData.MaxInitialSum = Convert.ToDecimal(depositModelData.MaxInitialSum.ToString("N1"));
            DataContext = depositModelData;
            
        }

        private void OpenDeposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePickerCalc.Text == "")
                    throw new Exception("Вы не указали дату окончания вклада!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= DateTime.Now)
                    throw new Exception("Дата окончания вклада указана неверно!");
                _depositUserData = new DepositUserData(_depositModelData, Convert.ToDecimal(sliderSum.Value), DateTime.Now, Convert.ToDateTime(datePickerCalc.Text));
                Deposit deposit = new Deposit(_depositUserData, _depositModelData, _person);
                deposit.Open();
                MessageBox.Show("Депозит оформлен!");
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
                    throw new Exception("Вы не указали дату окончания вклада!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= DateTime.Now)
                    throw new Exception("Дата окончания вклада указана неверно!");
                if (checkBox.IsChecked == true)
                {
                    PercentSumInEnd.Content = DepositCalculator.SumPercentInEndTermWithCapitalization(Convert.ToDecimal(sliderSum.Value), _depositModelData.AnnualRate/100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N2") + " ₽";
                    SumInEnd.Content = DepositCalculator.SumInEndTermWithCapitalization(Convert.ToDecimal(sliderSum.Value), _depositModelData.AnnualRate/100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N2") + " ₽";
                }
                else
                {
                    PercentSumInEnd.Content = DepositCalculator.SumPercentInEndTermWithoutCapitalization(Convert.ToDecimal(sliderSum.Value), _depositModelData.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N2") + " ₽";
                    SumInEnd.Content = DepositCalculator.SumInEndTermWithoutCapitalization(Convert.ToDecimal(sliderSum.Value), _depositModelData.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N2") + " ₽";
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
