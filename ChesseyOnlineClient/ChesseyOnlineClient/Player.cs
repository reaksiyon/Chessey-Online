using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesseyOnlineClient
{
    class Player : Program
    {
        static ChessTable TempTable = new ChessTable();
        Program program;
        public string myTeam = "null";
        public int sl_x, sl_y = 0;
        public char warType = 'N';
        public int process = 0;

        public void PlayerControl(string takenMSG)
        {

            //Console.WriteLine("CURRENT TEAM: " + takenMSG);
            //Console.WriteLine("YOUR TEAM: " + myTeam);


            if (takenMSG != myTeam + "_TURN")
            {
                //DEFENSE
                CTable.L2 = "\nDEFENSE TURN!\nMove with arrow keys and press enter to defense.";
                Program.Draw();
                ConsoleKeyInfo keyInfo;

                int x = 2;
                int y = 2;

                while (true)
                {
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        if (myTeam == "RED" && TempTable.tableColor[x, y] == 1)
                        {
                            // ATK

                            /*
                            while (CTable.table[x + 1, y] == '■')
                            {
                                x++;
                            }
                            */

                            //if (CTable.tableColor[x + 1, y] != 'P')
                            //CTable.tableColor[x + 1, y] = '■';
                            sl_x = x;
                            sl_y = y;
                            warType = 'D';
                            process = 1;
                            break;

                        }
                        else if (myTeam == "YELLOW" && TempTable.tableColor[x, y] == 2)
                        {
                            // ATK
                            sl_x = x;
                            sl_y = y;
                            warType = 'D';
                            process = 1;
                            break;
                        }


                    }



                    if (keyInfo.Key == ConsoleKey.RightArrow && y < 4)
                    {
                        y++;
                        CTable.tableColor[x, y - 1] = TempTable.tableColor[x, y - 1];

                    }

                    if (keyInfo.Key == ConsoleKey.LeftArrow && y > 0)
                    {
                        y--;
                        CTable.tableColor[x, y + 1] = TempTable.tableColor[x, y + 1];
                    }

                    if (keyInfo.Key == ConsoleKey.UpArrow && x > 0)
                    {
                        x--;
                        CTable.tableColor[x + 1, y] = TempTable.tableColor[x + 1, y];
                    }

                    if (keyInfo.Key == ConsoleKey.DownArrow && x < 4)
                    {
                        x++;
                        CTable.tableColor[x - 1, y] = TempTable.tableColor[x - 1, y];
                    }
                    CTable.tableColor[x, y] = 3;
                    Program.Draw();
                }
            }
            else
            {
                //ATTACK
                CTable.L2 = "\nATTACK TURN!\nMove with arrow keys and press enter to attack.";
                Program.Draw();
                ConsoleKeyInfo keyInfo;

                int x = 2;
                int y = 2;

                while (true)
                {
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        if (myTeam == "RED" && TempTable.tableColor[x, y] == 2)
                        {
                            // ATK

                            /*
                            while (CTable.tableColor[x + 1, y] == '■')
                            {
                                x++;
                            }

                            if (CTable.tableColor[x + 1, y] != 'P')
                                CTable.tableColor[x + 1, y] = '■';
                            */
                            sl_x = x;
                            sl_y = y;
                            warType = 'A';
                            process = 1;
                            break;
                        }
                        else if (myTeam == "YELLOW" && TempTable.tableColor[x, y] == 1)
                        {
                            // ATK
                            sl_x = x;
                            sl_y = y;
                            warType = 'A';
                            process = 1;
                            break;
                        }


                    }



                    if (keyInfo.Key == ConsoleKey.RightArrow && y < 4)
                    {
                        y++;
                        CTable.tableColor[x, y - 1] = TempTable.tableColor[x, y - 1];

                    }

                    if (keyInfo.Key == ConsoleKey.LeftArrow && y > 0)
                    {
                        y--;
                        CTable.tableColor[x, y + 1] = TempTable.tableColor[x, y + 1];
                    }

                    if (keyInfo.Key == ConsoleKey.UpArrow && x > 0)
                    {
                        x--;
                        CTable.tableColor[x + 1, y] = TempTable.tableColor[x + 1, y];
                    }

                    if (keyInfo.Key == ConsoleKey.DownArrow && x < 4)
                    {
                        x++;
                        CTable.tableColor[x - 1, y] = TempTable.tableColor[x - 1, y];
                    }
                    CTable.tableColor[x, y] = 3;

                    Program.Draw();

                }
            }
        }
    }
}
