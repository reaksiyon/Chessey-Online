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
              {'A', 'B', 'C', 'D', 'E'},
              {'■', '■', 'F', '■', '■'},
              {'■', '■', '■', '■', '■'},
              {'■', '■', 'V', '■', '■'},
              {'X', 'Y', 'Z', 'Q', 'W'},
       };

       public int[,] tableColor = new int[,]
       {
              {1, 1, 1, 1, 1},
              {0, 0, 1, 0, 0},
              {0, 0, 0, 0, 0},
              {0, 0, 2, 0, 0},
              {2, 2, 2, 2, 2},
       };

        public char[] soldiers = { 'A', 'B', 'C', 'D', 'E' ,'F', 'X', 'Y' ,'Z', 'Q', 'W', 'V' };

        public int[] health = {2, 2, 2, 2, 2, 5, 2, 2, 2, 2, 2, 5}; // Health Table A-B-C-D-E-F / X-Y-Z-Q-W-V

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
                    } else if (tableColor[i, j] == 1)
                    {
                        Console.Write(table[i, j], Color.Red);
                    } else
                    {
                        Console.Write(table[i, j], Color.Yellow);
                    }
                    //Console.Write("■", Color.White);
                    Console.Write(" ");
                }
                    
                    
                    Console.WriteLine();
            }

            // Draw Health Table
            Console.SetCursorPosition(0, 0);
            Console.Write("#-");
            Console.Write("Health Table");
            Console.Write("-#");
            for (int i=0; i<12; i++)
            {
                Console.SetCursorPosition(0, i+1);
                Console.Write("| ");
                Console.Write(soldiers[i]);
                Console.Write(' ');
                for (int j = 0; j < health[i]; j++)
                {
                    if(i<6)
                    {
                        Console.Write("■", Color.Red);
                    } else
                    {
                        Console.Write("■", Color.Yellow);
                    }
                    
                }
                Console.SetCursorPosition(15, i+1);
                Console.Write("|");

            }
            Console.SetCursorPosition(0, 13);
            Console.Write("----------------");
            Console.SetCursorPosition(0, 14);
            //Console.WriteLine(table[2, 0] + "  " + table[2, 1] + "  " + table[2, 2] + "  " + table[2, 3] + "  " + table[2, 4] + "  ");


        }
    }
}
