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

namespace EyeRobotControlApp
{
    /// <summary>
    /// Interaction logic for HomeShoulders.xaml
    /// </summary>
    public partial class HomeShoulders : Page
    {
        private readonly SerialComm serialComm;

        public HomeShoulders(SerialComm comm)
        {
            InitializeComponent();
            serialComm = comm;

            displShoulderPos.Text = serialComm.Get_Position();
            doneText.Text = "DONE";

            ManualCommandsVisibility(Visibility.Visible);
            shoulderStepperManualButton.Visibility = Visibility.Hidden;
            waitText.Visibility = Visibility.Hidden;
            wait_bar.Visibility = Visibility.Hidden;
            doneText.Visibility = Visibility.Hidden;
        }

        private void WaitForSteppers()
        {
            wait_bar.Visibility = Visibility.Visible;
            waitText.Visibility = Visibility.Visible;
            ManualCommandsVisibility(Visibility.Hidden);

            wait_bar.Value = 0;

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    System.Threading.Thread.Sleep(240);
                    //System.Threading.Thread.Sleep(10);
                    
                    Dispatcher.Invoke(() =>
                    {
                        string state = serialComm.Get_Mode();
                        if (state.Equals("Mode: MenuMode\r")) i = 100;
                        wait_bar.Value = i;
                        if (i == 100)
                        {
                            doneText.Visibility = Visibility.Visible;
                            shoulderStepperManualButton.Visibility = Visibility.Visible;
                        }
                    });
                }
            });
            
        }

        private void ManualCommandsVisibility(Visibility visibility)
        {
            homeShoulderSteppersButton.Visibility = visibility;
            moveShoulersTo_Layout.Visibility = visibility;
            moveShoulderButton.Visibility = visibility;
            displShoulderPos.Visibility = visibility;
            metersPSA.Visibility = visibility;
        }

        private void ShoulderStepperManualButton_Click(object sender, RoutedEventArgs e)
        {
            waitText.Visibility = Visibility.Hidden;
            wait_bar.Visibility = Visibility.Hidden;
            doneText.Visibility = Visibility.Hidden;
            shoulderStepperManualButton.Visibility = Visibility.Hidden;
            ManualCommandsVisibility(Visibility.Visible);

            serialComm.ChangeState(SerialComm.StateMachine.ShoulderSteperManual);
        }

        private void MoveShoulderButton_Click(object sender, RoutedEventArgs e)
        {
            bool tryForward = float.TryParse(disForwardInput.GetLineText(0), out float forward);
            bool tryRight = float.TryParse(disRightInput.GetLineText(0), out float right);
            bool tryUp = float.TryParse(disUpInput.GetLineText(0), out float up);
            
            if (tryForward & tryRight & tryUp)
            {
                if ((forward < 0) & (right < 0) & (up < 0))
                    MessageBox.Show("Shoulder positions cannot be negative!");
                else serialComm.Send_ToPosition(right, forward, up);
            }
            else MessageBox.Show("Error in reading positions!");
            
            displShoulderPos.Text = serialComm.Get_Position();
        }

        private void HomeSteppers_Click(object sender, RoutedEventArgs e)
        {
            serialComm.ChangeState(SerialComm.StateMachine.HomeShoulderSteppers);
            this.WaitForSteppers();
        }
    }
}