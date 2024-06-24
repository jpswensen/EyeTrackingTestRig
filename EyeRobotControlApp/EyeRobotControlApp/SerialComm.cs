﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;

namespace EyeRobotControlApp
{
    public class SerialComm
    {
        private readonly SerialPort serialPort;
        private readonly Keyboard keyboard;

        public enum StateMachine
        {
            MenuMode = 0,
            EyeServoCalibration = 1,
            //AutoMode =2,
            HomeShoulderSteppers = 3,
            ShoulderSteperManual = 4,
            //FindCoordinates = 5,
            //SetCoordinates = 6,
            EyeServoManual = 7,
            NeckStepperCalibration = 8,
            //MoveNeckToCalibratedPoint = 9,
            NeckStepperManual = 10,
            //NeckYawServoCalibration = 11,
            //NeckYawServoManual = 12
        };

        public SerialComm(string comPort, int baud)
        {
            serialPort = new SerialPort(comPort, baud, Parity.None, 8, StopBits.One);

            serialPort.Open();
            
            keyboard = new Keyboard();
        }

        public string Get_Position()
        {
            System.Threading.Thread.Sleep(100);
            serialPort.WriteLine("p");
            string returnPoint = serialPort.ReadLine(); // need to get rid of '\n' char
            return "(" + returnPoint.Remove(returnPoint.Length - 1) + ")";
        }

        public string Get_Mode()
        {
            //System.Threading.Thread.Sleep(100);
            serialPort.WriteLine("m");
            System.Threading.Thread.Sleep(10);

            return serialPort.BytesToRead < 1 ? "" : serialPort.ReadLine();
        }

        public bool In_MenuMode()
        {
            string mode = Get_Mode();
            return mode.Equals("Mode: MenuMode\r");
        }

        public void Send_GazePoint(string point)
        {
            serialPort.WriteLine("g" + point);
        }

        public void Send_ToPosition(string point)
        {
            // add catch if in shoulder mode only accept positive values
            serialPort.WriteLine("g" + point);
        }

        public void Send_ToPosition(float p1, float p2, float p3)
        {
            string coord = "(" + p1.ToString() + "," + p2.ToString() + "," + p3.ToString() + ")";
            Send_ToPosition(coord);
        }

        public void SendToCenter()
        {
            serialPort.WriteLine("c");
        }

        public void Send(string command)
        //temperary method used for generic commands
        {
            serialPort.WriteLine(command);
        }

        public void Send_CalDown()
        {
            serialPort.WriteLine("d");
        }

        public void Send_CalUp()
        {
            serialPort.WriteLine("u");
        }

        public void Type_Char(char keyToFind)
        {
            serialPort.WriteLine("g" + keyboard.GetKey(keyToFind));
        }

        public bool IsOpen() { return serialPort.IsOpen; }
        
        public void ChangeState(StateMachine newstate)
        {
            serialPort.WriteLine("b");
            serialPort.WriteLine("s" + ((int)newstate).ToString());
        }

        private class Keyboard
        {
            Dictionary<char, string> keyboardCoord = new Dictionary<char, string>();

            public Keyboard()
            {
                keyboardCoord.Add('Q', "(0,0)");
                keyboardCoord.Add('W', "(0,0)");
                keyboardCoord.Add('E', "(0,0)");
                keyboardCoord.Add('R', "(0,0)");
                keyboardCoord.Add('T', "(0,0)");
                keyboardCoord.Add('Y', "(0,0)");
                keyboardCoord.Add('U', "(0,0)");
                keyboardCoord.Add('I', "(0,0)");
                keyboardCoord.Add('O', "(0,0)");
                keyboardCoord.Add('P', "(0,0)");
                keyboardCoord.Add('A', "(0,0)");
                keyboardCoord.Add('S', "(0,0)");
                keyboardCoord.Add('D', "(0,0)");
                keyboardCoord.Add('F', "(0,0)");
                keyboardCoord.Add('G', "(0,0)");
                keyboardCoord.Add('H', "(0,0)");
                keyboardCoord.Add('J', "(0,0)");
                keyboardCoord.Add('K', "(0,0)");
                keyboardCoord.Add('L', "(0,0)");
                keyboardCoord.Add('Z', "(0,0)");
                keyboardCoord.Add('X', "(0,0)");
                keyboardCoord.Add('C', "(0,0)");
                keyboardCoord.Add('V', "(0,0)");
                keyboardCoord.Add('B', "(0,0)");
                keyboardCoord.Add('N', "(0,0)");
                keyboardCoord.Add('M', "(0,0)");
                keyboardCoord.Add(',', "(0,0)");
                keyboardCoord.Add('.', "(0,0)");
                keyboardCoord.Add(' ', "(0,0)");
            }

            public string GetKey(char key)
            {
                return keyboardCoord[key];
            }
        }

        public string GetPortName() { return serialPort.PortName; }
        public void Close() { serialPort.Close(); }

    }
}