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
using System.Windows.Shapes;
using System.IO.Ports;

namespace ControlApp
{
    /// <summary>
    /// Interaction logic for EyeCal.xaml
    /// </summary>
    public partial class EyeCal : Window
    {
        //private readonly SerialPort sp1 = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One); //test Arduino
        private readonly SerialPort sp1 = new SerialPort("COM3", 36400, Parity.None, 8, StopBits.One); //robot

        public EyeCal()
        {
            InitializeComponent();
            sp1.Open();
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("b");
            sp1.Close();
            this.Close();
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("u");
        }
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("l");
        }
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("r");
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("d");
        }

    }
}
