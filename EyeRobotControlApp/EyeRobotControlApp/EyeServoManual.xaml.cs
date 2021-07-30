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
    /// Interaction logic for EyeServoManual.xaml
    /// </summary>
    public partial class EyeServoManual : Page
    {
        public string DisplGazePos
        {
            get { return (string)GetValue(DisplGazePosProperty); }
            set { SetValue(DisplGazePosProperty, value); }
        }
        public static readonly DependencyProperty DisplGazePosProperty =
            DependencyProperty.Register("DisplGazePos", typeof(string), typeof(EyeServoManual), new PropertyMetadata(string.Empty));

        private readonly SerialComm serialComm;

        public EyeServoManual(SerialComm comm)
        {
            InitializeComponent();
            DataContext = this;

            serialComm = comm;

            eyeServoCalButton.Content = " Eye Servo\nCalibration";
            gButton.Content += "\n";
        }

        private void GazeButton_Click(object sender, RoutedEventArgs e)
        {
            bool tryX = short.TryParse(gazeToTextBoxX.GetLineText(0), out short posX);
            bool tryZ = short.TryParse(gazeToTextBoxZ.GetLineText(0), out short posZ);
            if (tryX & tryZ)
            {
                string newpos = ((float)posX / 1000).ToString() + "," + ((float)posZ / 1000).ToString();
                serialComm.Send_GazePoint(newpos);
                DisplGazePos = serialComm.Get_Position();
            }
            else
            {
                MessageBox.Show("Cannot regognize X or Z inputs!");
            }

        }

        private void PosButton_Click(object sender, RoutedEventArgs e)
        {
            DisplGazePos = serialComm.Get_Position();
        }

        private void CenterButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.SendToCenter();
            DisplGazePos = serialComm.Get_Position();
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            //DisplMode = serialComm.Get_Mode();
        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            string phrase = letterTextBox.Text.ToString().ToUpperInvariant();
            MessageBox.Show(phrase);

            foreach (char toType in phrase)
            { // note: won't display position during loop without Messagebox, even if delayed
                serialComm.Type_Char(toType);
                this.DisplGazePos = serialComm.Get_Position();
                //time for tobii to recognize the chosen key
                MessageBox.Show(toType.ToString());
            }
        }

        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.Send(debugText.Text.ToString());
        }

        private void EyeServoCal_Click(object sender, RoutedEventArgs e)
        {
            EyeServoCalibration eyeServoCalibration = new EyeServoCalibration(serialComm);
            serialComm.ChangeState(SerialComm.StateMachine.EyeServoCalibration);
            eyeServoCalibration.ShowDialog();
        }
    }

    
}
