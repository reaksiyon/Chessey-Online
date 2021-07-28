using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Telepathy;

using Console = Colorful.Console;

namespace ChesseyOnlineClient
{
    class Program
    {
        static ChessTable CTable = new ChessTable();

        Telepathy.Client client = new Telepathy.Client();
        Telepathy.Message msg;

        static void Main(string[] args)
        {
            //InitGame();

            //Draw();
            //StartGame();
            
            Program program = new Program();

            string IP = "";

            Console.Write("Connect IP: ");

            IP = Console.ReadLine();

            program.serverConnect(IP);

            Thread.Sleep(50);

            program.SendClientMSG("");

            while(true)
            { 
            program.serverGetMsg();
                Thread.Sleep(50);
            }
        }

        public void serverConnect(string ip)
        {
            client.Connect(ip, 1337); // ip address

            serverGetMsg();
            System.Threading.Thread.Sleep(1000);
        }

        public static void StartGame()
        {
            Console.SetCursorPosition(20, 6);
            Console.WriteLine("");

            


        }

        public static void RoundCheck(bool team)
        {
            

        }


        public static void InitGame()
        {
            Draw();
        }

        public static void PlaySound()
        {
            Console.Beep(261, 250);
            Console.Beep(329, 250);
            Console.Beep(261, 250);
            Console.Beep(329, 250);

            Console.Beep(392, 250);
            System.Threading.Thread.Sleep(350);
            Console.Beep(392, 250);

            Console.Beep(261, 250);
            Console.Beep(329, 250);
            Console.Beep(261, 250);
            Console.Beep(329, 250);

            Console.Beep(392, 250);
            System.Threading.Thread.Sleep(250);
            Console.Beep(392, 250);

            Console.Beep(261, 250);
            Console.Beep(493, 250);
            Console.Beep(440, 250);
            Console.Beep(392, 250);
            Console.Beep(349, 250);
            System.Threading.Thread.Sleep(50);
            Console.Beep(440, 250);
            Console.Beep(392, 250);
            Console.Beep(349, 250);
            Console.Beep(329, 250);
            Console.Beep(293, 250);
            Console.Beep(261, 250);
            System.Threading.Thread.Sleep(100);
            Console.Beep(261, 250);

        }

        public static void Draw()
        {
            CTable.Draw();
        }


        public static void LoginThread()
        {
            CTable.LoginScreenDraw();
            Thread.Sleep(0);
        }


        public void serverGetMsg()
        {
            
            while (client.GetNextMessage(out msg))
            {

                switch (msg.eventType)
                {
                    case Telepathy.EventType.Connected:
                        //Console.Clear();

                        //Console.WriteLine("DEBUG: CONNECTED!");
                        break;

                    case Telepathy.EventType.Data: //msg taken

                        string takenMSG = Encoding.ASCII.GetString(msg.data);

                        Console.WriteLine(takenMSG);

                        if(takenMSG == "all.connected")
                        {
                            Console.Clear();

                            Thread LoginScreen = new Thread(new ThreadStart(LoginThread));

                            LoginScreen.Start();

                            PlaySound();
                        }

                        break;


                    case Telepathy.EventType.Disconnected:

                        //Console.Clear();
                        Console.WriteLine("Connection Error!");
                        break;
                }
            }
        }

        public void SendClientMSG(string msg)
        {
            byte[] msgc = Encoding.Default.GetBytes(msg);

            client.Send(msgc);
            serverGetMsg();
        }

    }

}
