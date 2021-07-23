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
              {'■', '■', '■', '■', '■'},
              {'■', '■', '■', '■', '■'},
              {'■', '■', '■', '■', '■'},
              {'■', '■', '■', '■', '■'},
              {'■', '■', '■', '■', '■'},
            };

        public void Draw(Color clr)
        {
            Console.Clear();

            if (clr == Color.Red)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (table[i, j] != '■')
                        {
                            Console.Write(table[i, j] + "  ", clr);
                        }
                        else
                        {
                            Console.Write(table[i, j] + "  ");
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine(table[2, 0] + "  " + table[2, 1] + "  " + table[2, 2] + "  " + table[2, 3] + "  " + table[2, 4] + "  ");


            }
            else
            {
                Console.WriteLine(table[2, 0] + "  " + table[2, 1] + "  " + table[2, 2] + "  " + table[2, 3] + "  " + table[2, 4] + "  ");

                for (int i = 2; i < 5; i++)
                {

                    for (int j = 0; j < 5; j++)
                    {
                        if (table[i, j] != '■')
                        {
                            Console.Write(table[i, j] + "  ", clr);
                        }
                        else
                        {
                            Console.Write(table[i, j] + "  ");
                        }
                    }

                    Console.WriteLine();
                }

            }



        }
    }
}
