using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;

namespace ChesseyOnlineClient
{
    class Program
    {
        static ChessTable CTable = new ChessTable();
        static Team RedTeam = new Team();
        static Team YellowTeam = new Team();

        static void Main(string[] args)
        {
            InitGame();

            int sg1 = 0,sg2 = 0,sg3 = 0,sg4 = 0;

            Console.WriteLine();
            Console.WriteLine("Sayi gir 4 tn");

            sg1 = Int32.Parse(Console.ReadLine());
            sg2 = Int32.Parse(Console.ReadLine());
            sg3 = Int32.Parse(Console.ReadLine());
            sg4 = Int32.Parse(Console.ReadLine());

            CTable.table[sg3, sg4] = CTable.table[sg1,sg2];
            CTable.table[sg1,sg2] = '■';

            Draw(Color.Yellow);
            System.Console.Read();
        }


        public static void InitGame()
        {
            
            //team A
            CTable.table[0,0] = RedTeam.Pieces[0];
            CTable.table[0, 1] = RedTeam.Pieces[1];
            CTable.table[0, 2] = RedTeam.Pieces[2];
            CTable.table[0, 3] = RedTeam.Pieces[1];
            CTable.table[0, 4] = RedTeam.Pieces[0];
            CTable.table[1, 1] = RedTeam.Pieces[3];
            CTable.table[1, 2] = RedTeam.Pieces[3];
            CTable.table[1, 3] = RedTeam.Pieces[3];
            Draw(Color.Red);

            //team B
            CTable.table[4, 0] = YellowTeam.Pieces[0];
            CTable.table[4, 1] = YellowTeam.Pieces[1];
            CTable.table[4, 2] = YellowTeam.Pieces[2];
            CTable.table[4, 3] = YellowTeam.Pieces[1];
            CTable.table[4, 4] = YellowTeam.Pieces[0];
            CTable.table[3, 1] = YellowTeam.Pieces[3];
            CTable.table[3, 2] = YellowTeam.Pieces[3];
            CTable.table[3, 3] = YellowTeam.Pieces[3];
            Draw(Color.Yellow);
        }


        public static void Draw(Color clr)
        {
            CTable.Draw(clr);
        }


    }
}
