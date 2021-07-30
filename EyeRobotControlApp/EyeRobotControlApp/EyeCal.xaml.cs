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

namespace EyeRobotControlApp
{
    /// <summary>
    /// Interaction logic for EyeCal.xaml
    /// </summary>
    public partial class EyeCal : Window
    {
        private SerialComm serialComm;

        public EyeCal(SerialComm comm)
        {
            InitializeComponent();
            serialComm = comm;
            up_button.IsEnabled = false;
            down_button.IsEnabled = false;
            right_button.IsEnabled = false;
            left_button.IsEnabled = false;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("B");
            this.Close();
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("U");
        }
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("L");
        }
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("R");
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("D");
        }

        private void LeftVerticalServo_Click(object sender, RoutedEventArgs e)
        {
            this.up_button.IsEnabled = true;
            this.down_button.IsEnabled = true;
            this.left_button.IsEnabled = false;
            this.right_button.IsEnabled = false;

            serialComm.Send("1"); /*go to LV servo*/
        }

        private void RightVerticalServo_Click(object sender, RoutedEventArgs e)
        {
            this.up_button.IsEnabled = true;
            this.down_button.IsEnabled = true;
            this.left_button.IsEnabled = false;
            this.right_button.IsEnabled = false;

            serialComm.Send("2"); /*go to RV servo*/
        }

        private void LeftHorizontalServo_Click(object sender, RoutedEventArgs e)
        {
            this.up_button.IsEnabled = false;
            this.down_button.IsEnabled = false;
            this.left_button.IsEnabled = true;
            this.right_button.IsEnabled = true;

            serialComm.Send("3"); /*go to LH servo*/
        }

        private void RightHorizontalServo_Click(object sender, RoutedEventArgs e)
        {
            this.up_button.IsEnabled = false;
            this.down_button.IsEnabled = false;
            this.left_button.IsEnabled = true;
            this.right_button.IsEnabled = true;

            serialComm.Send("4"); /*go to RH servo*/
        }
    }
}
