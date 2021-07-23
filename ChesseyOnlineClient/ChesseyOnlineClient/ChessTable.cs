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

        public char[] soldiers = { 'H', 'I', 'X', 'I', 'H' ,'O', 'O', 'O', 'H', 'I', 'X', 'I', 'H', 'O', 'O', 'O', };

        public int[] health = {2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 5, 2, 2, 2, 2, 2}; // Health Table

        public void Draw()
        {
            Console.Clear();

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(20, i);

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

            // Draw Health Table
            Console.SetCursorPosition(0, 0);

            Console.Write("#-");

            Console.Write("Health Table");

            Console.Write("-#");

            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(0, i + 1);

                Console.Write("| ");

                Console.Write(soldiers[i]);

                Console.Write(' ');

                for (int j = 0; j < health[i]; j++)
                {
                    if (i < 8)
                    {
                        Console.Write("■", Color.Red);
                    }
                    else
                    {
                        Console.Write("■", Color.Yellow);
                    }

                }

                Console.SetCursorPosition(15, i + 1);

                Console.Write("|");

            }

            Console.SetCursorPosition(0, 17);

            Console.Write("----------------");

            Console.SetCursorPosition(0, 18);

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
