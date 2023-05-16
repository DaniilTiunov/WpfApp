using System;
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
using System.IO.Ports;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();

        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            try
            {
                ComPort comPort = new ComPort();

                string portName =  comboBoxPorts.SelectedValue.ToString();
                int baudRate = Convert.ToInt32(comboBoxBaudRate.SelectedItem.ToString());
                int dataBits = Convert.ToInt32(comboBoxStopBit.SelectedItem.ToString());
                Parity parity = Parity.Even;
                StopBits stopBits = StopBits.One;
                Handshake portHandshake = Handshake.None;             

                comPort.Open(
                    portName,
                    baudRate, 
                    dataBits, 
                    parity,
                    stopBits,
                    portHandshake);
            }
            
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonUpdatePorts_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                string[] Ports = Helpers.GetAvailablePorts();
                foreach (string Port in Ports)
                {
                    comboBoxPorts.Items.Add(Port);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }
      
    }   
}   
