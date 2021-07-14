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
//using System.Windows.Forms;

namespace ControlApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly SerialPort sp1 = new SerialPort("COM3",9600,Parity.None,8,StopBits.One); //test Arduino
        private readonly SerialPort sp1 = new SerialPort("COM3", 36400, Parity.None, 8, StopBits.One); //robot

        public string DisplPos
        {
            get { return (string)GetValue(DisplPosProperty); }
            set { SetValue(DisplPosProperty, value); }
        }
        public static readonly DependencyProperty DisplPosProperty =
            DependencyProperty.Register("DisplPos", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public string DisplMode
        {
            get { return (string)GetValue(DisplModeProperty); }
            set { SetValue(DisplModeProperty, value); }
        }
        public static readonly DependencyProperty DisplModeProperty =
            DependencyProperty.Register("DisplMode", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            //this.Displ = "test1";

            sp1.Open();

            if (sp1.IsOpen)
            {
                MessageBox.Show(sp1.PortName + " is open!");
            }
            else
            {
                MessageBox.Show("Mission failed! no port found");
            }

            Get_Mode();
        }

        private void GazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!sp1.IsOpen) sp1.Open();
            string newpos = posTextBox.Text.ToString();
            //MessageBox.Show(mytext);
            sp1.WriteLine("g" + newpos);
            //MessageBox.Show(sp1.ReadExisting());

            Get_Position();
            //Set_Mode();
        }

        private void PosButton_Click(object sender, RoutedEventArgs e) 
        { 
            if (!sp1.IsOpen) sp1.Open();
            Get_Position();
        }
        private void Get_Position()
        {
            System.Threading.Thread.Sleep(100);
            sp1.WriteLine("p");
            //MessageBox.Show(sp1.ReadExisting());
            this.DisplPos = sp1.ReadLine();
        }

        private void CenterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!sp1.IsOpen) sp1.Open();
            sp1.WriteLine("c");
            Get_Position();
        }

        private void ModeButton_Click(object sender, RoutedEventArgs e) { }
        private void Get_Mode()
        {
            System.Threading.Thread.Sleep(100);
            sp1.WriteLine("m");
            //MessageBox.Show(sp1.ReadExisting());
            this.DisplMode = sp1.ReadLine();
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!sp1.IsOpen) sp1.Open();
            string word = letterTextBox.Text.ToString().ToUpperInvariant();

            foreach (var letter in word)
            {
                if (letter.Equals('Q'))
                {
                    //MessageBox.Show(((int)letter).ToString());
                    MessageBox.Show("Queen");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('W'))
                {
                    MessageBox.Show("Water");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('E'))
                {
                    MessageBox.Show("Elk");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('R'))
                {
                    MessageBox.Show("Runs");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('T'))
                {
                    MessageBox.Show("Through");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('Y'))
                {
                    MessageBox.Show("You");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('U'))
                {
                    MessageBox.Show("Under");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('I'))
                {
                    MessageBox.Show("Iota");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('O'))
                {
                    MessageBox.Show("Ostrich");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('P'))
                {
                    MessageBox.Show("Penultimate");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('A'))
                {
                    MessageBox.Show("Alpha");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('S'))
                {
                    MessageBox.Show("Slimey");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('D'))
                {
                    MessageBox.Show("Delaware");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('F'))
                {
                    MessageBox.Show("Ferrum");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('G'))
                {
                    MessageBox.Show("Grill");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('H'))
                {
                    MessageBox.Show("House");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('J'))
                {
                    MessageBox.Show("Jimmy John's");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('K'))
                {
                    MessageBox.Show("Kinetic");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('L'))
                {
                    MessageBox.Show("Legs");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('Z'))
                {
                    MessageBox.Show("Zulu");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('X'))
                {
                    MessageBox.Show("X-Ray");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('C'))
                {
                    MessageBox.Show("Center");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('V'))
                {
                    MessageBox.Show("Venus");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('B'))
                {
                    MessageBox.Show("Boy");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('N'))
                {
                    MessageBox.Show("Newton");
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals('M'))
                {
                    sp1.WriteLine("g" + Keyboard.q);
                }
                else if (letter.Equals(','))
                {
                    MessageBox.Show(",");
                    sp1.WriteLine("g" + Keyboard.comma);
                }
                else if (letter.Equals('.'))
                {
                    MessageBox.Show(".");
                    sp1.WriteLine("g" + Keyboard.stop);
                }
                else if (letter.Equals('1'))
                {
                    MessageBox.Show("top center");
                    sp1.WriteLine("g5,35");
                }
                else if (letter.Equals('2'))
                {
                    MessageBox.Show("bottom left");
                    sp1.WriteLine("g-70,-32");
                }
                else if (letter.Equals('3'))
                {
                    MessageBox.Show("bottom left");
                    sp1.WriteLine("g60,-40");
                }
                Get_Position();
            }
        }

        private void EyeCal_Click(object sender, RoutedEventArgs e)
        {
            sp1.WriteLine("s1");
            sp1.Close();
            EyeCal eyecal = new EyeCal();
            eyecal.Show();
        }
    }

    class Keyboard
    {
        public static string q = "-65,0";
        public static string w = "";
        public static string r = "";
        public static string t = "";
        public static string y = "";
        public static string u = "";
        public static string i = "";
        public static string o = "";
        public static string p = "";
        public static string a = "";
        public static string s = "";
        public static string d = "";
        public static string f = "";
        public static string g = "";
        public static string h = "";
        public static string j = "";
        public static string k = "";
        public static string l = "";
        public static string z = "";
        public static string x = "";
        public static string c = "";
        public static string v = "";
        public static string b = "";
        public static string n = "";
        public static string m = "";
        public static string comma = "";
        public static string stop = "";
    }
}