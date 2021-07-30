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
using System.Runtime.InteropServices;

namespace EyeRobotControlApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string DisplMode
        {
            get { return (string)GetValue(DisplModeProperty); }
            set { SetValue(DisplModeProperty, value); }
        }
        public static readonly DependencyProperty DisplModeProperty =
            DependencyProperty.Register("DisplMode", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        private SerialComm serialComm;

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = this;
            Background = new ImageBrush(SetImage(Properties.Resources.colorSplash));

            try
            {
                //serialComm = new SerialComm("COM4", 9600); // test Arduino
                serialComm = new SerialComm("COM3", 115200); // robot

                if (serialComm.IsOpen())
                {
                    MessageBox.Show(serialComm.GetPortName() + " is open!");
                    //DisplMode = serialComm.Get_Mode();
                }
            }
            catch (Exception e)
            {
                DisplMode = "no Arduino found";
                MessageBox.Show("Program failed, not connected to Arduino\n");
                this.Close();
            }
            
        }

        private void EyeModeButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.ChangeState(SerialComm.StateMachine.EyeServoManual);
            //DisplMode = serialComm.Get_Mode();
            DisplayPage.Content = new EyeServoManual(serialComm);
            ShowClick(eyeModeButton);
        }

        private void ShoulderModeButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.ChangeState(SerialComm.StateMachine.ShoulderSteperManual);
            HomeShoulders homeShoulders = new HomeShoulders(serialComm);
            DisplayPage.Content = homeShoulders; 
            ShowClick(shoudlerModeButton);
            //homeShoulders.WaitForSteppers();
        }

        private void NeckSteppersManualButton_Click(object sender, RoutedEventArgs e)
        {
            serialComm.ChangeState(SerialComm.StateMachine.NeckStepperManual);
            DisplayPage.Content = new NeckSteppersManual(serialComm);
            ShowClick(neckModeButton);
        }

        private void ShowClick(Button button_clicked)
        {
            eyeModeButton.IsEnabled = true;
            neckModeButton.IsEnabled = true;
            shoudlerModeButton.IsEnabled = true;

            button_clicked.IsEnabled = false;
        }

        private ImageSource SetImage(System.Drawing.Bitmap picture)
        {
            BitmapSource bSource = BitmapToBitmapSource(picture);
            return bSource;
        }

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            serialComm.In_MenuMode();
        }
    }
}