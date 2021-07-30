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

namespace EyeRobotControlApp
{
    /// <summary>
    /// Interaction logic for NeckStepperNServoCalibration.xaml
    /// </summary>
    public partial class NeckStepperNServoCalibration : Window
    {
        private SerialComm serialComm;

        public NeckStepperNServoCalibration(SerialComm serialComm)
        {
            this.serialComm = serialComm;
            InitializeComponent();

            pitchStepper.Content = "Front Stepper\nPitch";
            rollStepperL.Content = "Left Stepper\nRoll Left";
            rollStepperR.Content = "Right Stepper\nRoll Right";
        }

        private void PitchStepper_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("1");
        }

        private void RollStepperL_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("3");
        }

        private void RollStepperR_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("2");
        }

        private void NeckUp_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send_CalUp();
        }

        private void NeckDown_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send_CalDown();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("z"); //sets EPROM value to current location
            serialComm.ChangeState(SerialComm.StateMachine.NeckStepperManual);
            this.Close();
        }

        private void YawServo_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send("4");
        }

        private void YawLeft_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send_CalDown();
        }

        private void YawRight_CLick(object sender, RoutedEventArgs e)
        {
            serialComm.Send_CalUp();
        }
    }
}
