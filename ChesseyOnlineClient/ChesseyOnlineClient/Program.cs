using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace ChesseyOnlineClient
{
    class Program
    {
        static ChessTable CTable = new ChessTable();

        static void Main(string[] args)
        {
            InitGame();

            Console.Clear();

            Thread LoginScreen = new Thread(new ThreadStart(LoginThread));
            LoginScreen.Start();

            StartGame();

            System.Console.Read();
        }


        public static void StartGame()
        {
            PlaySound();
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

    }

}
