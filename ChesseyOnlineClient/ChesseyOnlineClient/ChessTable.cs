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
        public String L1 = "";
        public String L2 = "";
        public String L3 = "";

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
              {1, 1, 1, 1, 1}, // 1 red
              {0, 1, 1, 1, 0}, // 2 yellow
              {0, 0, 0, 0, 0},
              {0, 2, 2, 2, 0},
              {2, 2, 2, 2, 2},
       };

       public int[,] TeamTable = new int[,]
        {
              {1, 1, 1, 1, 1}, // 1 red team
              {0, 1, 1, 1, 0}, // 2 yellow team
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

                    if (tableColor[i, j] == 0)
                    {
                        Console.Write(table[i, j], Color.White);
                        //Console.Write("Şuan beyaz oldu");

                    }
                    else if (tableColor[i, j] == 1)
                    {
                        Console.Write(table[i, j], Color.Red);
                    }
                    else if (tableColor[i, j] == 2)
                    {
                        Console.Write(table[i, j], Color.Yellow);
                    } else
                    {
                        Console.Write(table[i, j], Color.Orange);
                    }

                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(L1, Color.LightGreen);
            Console.WriteLine(L2, Color.Aqua);
            Console.WriteLine(L3, Color.Gold);

        }

        public void LoginScreenDraw()
        {
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Random rnd = new Random();
                    Color[] ColorList = { Color.Orange, Color.Tomato, Color.Pink, Color.Purple, Color.LightBlue };
                    
                    //Color randomColor = Color.FromArgb(255, rnd.Next(0,255), rnd.Next(0,255), rnd.Next(0, 255));


                    //Console.ForegroundColor = randomColor;
                    Console.Write(table[i, j] + "  ", ColorList[j]);

                    System.Threading.Thread.Sleep(200);
                }
                Console.WriteLine();
            }
            
            Draw();
        }
    }
}
