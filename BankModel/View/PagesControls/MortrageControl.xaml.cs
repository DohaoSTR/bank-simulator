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
using Test.Persons.Models;

namespace BankModel.View.PagesControls
{
    /// <summary>
    /// Логика взаимодействия для MortrageControl.xaml
    /// </summary>
    public partial class MortrageControl : UserControl
    {
        private MortrageModel _mortrageModel;
        private MortrageUser _mortrageUser;
        private IPerson _person;

        public MortrageControl(MortrageModel mortrageModel, IPerson person)
        {
            InitializeComponent();

            _mortrageModel = mortrageModel;
            _person = person;
            mortrageModel.AnnualRate = Convert.ToDecimal((mortrageModel.AnnualRate * 100).ToString("N1"));
            mortrageModel.MinInitialSum = Convert.ToDecimal(mortrageModel.MinInitialSum.ToString("N1"));
            mortrageModel.MaxInitialSum = Convert.ToDecimal(mortrageModel.MaxInitialSum.ToString("N1"));
            mortrageModel.MinInitialFee = Convert.ToDecimal(mortrageModel.MinInitialFee.ToString("N1"));
            mortrageModel.MaxInitialFee = Convert.ToDecimal(mortrageModel.MaxInitialFee.ToString("N1"));
            mortrageModel.TypePayment = "Аннуитетный платеж";
            DataContext = mortrageModel;
        }

        private void OpenMortrage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePickerCalc.Text == "")
                    throw new Exception("Вы не указали дату окончания ипотеки!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= _mortrageModel.MinTermEnd || Convert.ToDateTime(datePickerCalc.Text) >= _mortrageModel.MaxTermEnd)
                    throw new Exception("Дата окончания ипотеки указана неверно!");
                _mortrageUser = new MortrageUser(Convert.ToDecimal(sliderSum.Value), DateTime.Now, Convert.ToDateTime(datePickerCalc.Text), _mortrageModel, Convert.ToDecimal(sliderFee.Value));
                Mortrage mortrage = new Mortrage(_mortrageModel, _mortrageUser, _person);
                mortrage.Open();
                MessageBox.Show("Ипотека оформлена!");
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
                    throw new Exception("Вы не указали дату окончания ипотеки!");
                if (Convert.ToDateTime(datePickerCalc.Text) <= DateTime.Now)
                    throw new Exception("Дата окончания ипотеки указана неверно!");
                SumCredit.Content = CreditCalculator.AnnuitetSumPaymentWithInitialFee(Convert.ToDecimal(sliderSum.Value), Convert.ToDecimal(sliderFee.Value), _mortrageModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                MonthlyPayment.Content = CreditCalculator.MonthlyPaymentAnnuitetWithInitialFee(Convert.ToDecimal(sliderSum.Value), Convert.ToDecimal(sliderFee.Value), _mortrageModel.AnnualRate / 100, DateTime.Now, Convert.ToDateTime(datePickerCalc.Text)).ToString("N1") + " ₽";
                MessageBox.Show("Расчет произведён!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
