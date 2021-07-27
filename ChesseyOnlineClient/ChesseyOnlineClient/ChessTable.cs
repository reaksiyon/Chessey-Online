using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;

namespace ChesseyOnlineClient
{
    class ChessTable
    {

        public char[,] table = new char[,]
       {
              {'H', 'I', 'X', 'I', 'H'},
              {'■', 'O', 'O', 'O', '■'},
              {'■', '■', '■', '■', '■'},
              {'■', 'O', 'O', 'O', '■'},
              {'H', 'I', 'X', 'I', 'H'},
       };

       public int[,] tableColor = new int[,]
       {
              {1, 1, 1, 1, 1},
              {0, 1, 1, 1, 0},
              {0, 0, 0, 0, 0},
              {0, 2, 2, 2, 0},
              {2, 2, 2, 2, 2},
       };

        public void Draw()
        {
            Console.Clear();

            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" ");

                    if (tableColor[i, j] == 0)
                    {
                        Console.Write(table[i, j], Color.White);

                    }
                    else if (tableColor[i, j] == 1)
                    {
                        Console.Write(table[i, j], Color.Red);
                    }
                    else
                    {
                        Console.Write(table[i, j], Color.Yellow);
                    }

                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

        public void LoginScreenDraw()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Random rnd = new Random();

                    Color randomColor = Color.FromArgb(rnd.Next(0,256), rnd.Next(0,256), rnd.Next(0,256));

                    Console.ForegroundColor = randomColor;

                    Console.Write(table[i, j] + "  ");

                    System.Threading.Thread.Sleep(250);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = Color.White;

            Draw();
        }
    }
}
