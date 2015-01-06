using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ThermometerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ThermometerMonitor thermometer;
        TemperatureSensor sensor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thermometer = new ThermometerMonitor();

            thermometer.MaxAllowedTemperatureReached += handleThermometerAlert;
            thermometer.MinAllowedTemperatureReached += handleThermometerAlert;

            sensor = new TemperatureSensor(thermometer);

            Thread sensorThread = new Thread(sensor.On);

            sensorThread.Start();
        }

        private void handleThermometerAlert()
        {
            if (!this.Dispatcher.CheckAccess())
            {
                // Spørg Bjørk om det her! Hvad er det rigtige argument?!
                this.Dispatcher.Invoke(handleThermometerAlert);
                    //new ThermometerMonitor.TemperatureAlert(handleThermometerAlert));

                Action x = this.handleThermometerAlert;

                return;
            }

            alertTextBox.Text += String.Format("Temperature warning! {0} degrees!{1}",
                thermometer.CurrentTemperature, Environment.NewLine);
        }

        private void readTemperaturesButton_Click(object sender, RoutedEventArgs e)
        {
            readTemperatures();
        }

        private void readTemperatures()
        {
            currentTemperatureLabel.Content = thermometer.CurrentTemperature;
            minimumTemperatureLabel.Content = thermometer.MinTemperature;
            maximumTemperatureLabel.Content = thermometer.MaxTemperature;

            minAllowedTemperatureTextBox.Text = thermometer.MinAllowedTemperature.ToString();
            maxAllowedTemperatureTextBox.Text = thermometer.MaxAllowedTemperature.ToString();
        }

        private void setAllowedTemperatures_Click(object sender, RoutedEventArgs e)
        {
            int minAllowedTemperature = int.Parse(minAllowedTemperatureTextBox.Text);
            int maxAllowedTemperature = int.Parse(maxAllowedTemperatureTextBox.Text);

            thermometer.MinAllowedTemperature = minAllowedTemperature;
            thermometer.MaxAllowedTemperature = maxAllowedTemperature;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            thermometer.Clear();
            alertTextBox.Text = "";
            
            readTemperatures();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sensor.Off();
        }
    }
}
