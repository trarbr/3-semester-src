﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValutaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ValutaWcfService.IValutaService valutaService;
        private ValutaWcfService.Valuta selectedValuta;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            valutaService = new ValutaWcfService.ValutaServiceClient();

            refreshUI();
        }

        private void refreshUI()
        {
            ValutaWcfService.Valuta[] valutas = valutaService.GetValutas();

            masterView.ItemsSource = null;
            masterView.ItemsSource = valutas;

            fromValutaComboBox.Items.Clear();
            toValutaComboBox.Items.Clear();

            foreach (ValutaWcfService.Valuta valuta in valutas)
            {
                fromValutaComboBox.Items.Add(valuta.Iso);
                toValutaComboBox.Items.Add(valuta.Iso);
            }
        }

        private void masterView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = masterView.SelectedItem;
            if (selectedItem != null)
            {
                selectedValuta = (ValutaWcfService.Valuta)selectedItem;
                nameTextBox.Text = selectedValuta.Name;
                isoTextBox.Text = selectedValuta.Iso;
                exchangeRateTextBox.Text = selectedValuta.ExchangeRate.ToString("N2");
                addValutaButton.IsEnabled = false;
                setExchangeRateButton.IsEnabled = true;
            }
            else
            {
                clearTextBoxes();
                selectedValuta = null;
                addValutaButton.IsEnabled = true;
                setExchangeRateButton.IsEnabled = false;
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            masterView.SelectedItem = null;
        }

        private void addValutaButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string iso = isoTextBox.Text.Trim();
            decimal exchangeRate;
            decimal.TryParse(exchangeRateTextBox.Text, out exchangeRate);
            ValutaWcfService.Valuta valuta = new ValutaWcfService.Valuta()
            {
                Name = name,
                Iso = iso,
                ExchangeRate = exchangeRate
            };

            bool added = valutaService.AddValuta(valuta);
            if (!added)
            {
                MessageBox.Show("A valuta with the specified ISO already exists. Please find and update the existing one");
            }
            else
            {
                clearTextBoxes();
            }
            refreshUI();
        }

        private void clearTextBoxes()
        {
            nameTextBox.Text = "";
            isoTextBox.Text = "";
            exchangeRateTextBox.Text = "";
        }

        private void setExchangeRateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedValuta != null)
            {
                decimal exchangeRate;
                decimal.TryParse(exchangeRateTextBox.Text, out exchangeRate);
                selectedValuta.ExchangeRate = exchangeRate;
                bool updated = valutaService.SetValutaExchangeRate(selectedValuta);
                if (!updated)
                {
                    MessageBox.Show("Something went wrong. Please try again.");
                }
                refreshUI();
            }
            else
            {
                MessageBox.Show("Please select a valuta from the box on the left.");
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            refreshUI();
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            decimal fromAmount;
            decimal.TryParse(fromAmountTextBox.Text, out fromAmount);
            string fromIso = (string)fromValutaComboBox.SelectedItem;
            string toIso = (string)toValutaComboBox.SelectedItem;

            decimal toAmount = valutaService.ConvertFromIsoToIso(fromIso, toIso, fromAmount);

            toAmountTextBox.Text = toAmount.ToString("N2");
        }

        private void listConversionsButton_Click(object sender, RoutedEventArgs e)
        {
            string[] conversions = valutaService.GetDoneConversions();
            conversionsListBox.ItemsSource = null;
            conversionsListBox.ItemsSource = conversions;
        }
    }
}
