using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesseyOnlineClient
{
    class Music
    {

        public void PlaySound()
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

    }
}
