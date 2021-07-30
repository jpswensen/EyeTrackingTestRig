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
    /// Interaction logic for NeckSteppersManual.xaml
    /// </summary>
    public partial class NeckSteppersManual : Page
    {
        private SerialComm serialComm;

        public NeckSteppersManual(SerialComm serialComm)
        {
            this.serialComm = serialComm;
            InitializeComponent();
            neckPosition.Text = this.serialComm.Get_Position();
        }

        private void NeckCalibrationButton_Click(object sender, RoutedEventArgs e)
        {
            NeckStepperNServoCalibration neckCal = new NeckStepperNServoCalibration(serialComm);
            serialComm.ChangeState(SerialComm.StateMachine.NeckStepperCalibration);
            neckCal.ShowDialog();
        }

        private void sendAngles_Click(object sender, RoutedEventArgs e)
        {
            bool tryYaw = float.TryParse(yawInput.GetLineText(0), out float yaw);
            bool tryPitch = float.TryParse(pitchInput.GetLineText(0), out float pitch);
            bool tryRoll = float.TryParse(rollInput.GetLineText(0), out float roll);

            // TODO: add another check that values are within valid range
            if (tryYaw & tryPitch & tryRoll)
            {
                serialComm.Send_ToPosition(roll, pitch, yaw);
                neckPosition.Text = serialComm.Get_Position();
            }
            else MessageBox.Show("Those inputs weren't regocnizable!\nYou need to give one angle per box.");
        }
    }
}