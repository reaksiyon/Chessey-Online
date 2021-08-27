using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using Telepathy;

//using Console = Colorful.Console;

namespace ChesseyOnlineClient
{
    class Program
    {
        public static ChessTable CTable = new ChessTable();

        static Player player = new Player();
        static Music music = new Music();

        Telepathy.Client client = new Telepathy.Client();
        Telepathy.Message msg;

        

        bool isGameStart = false;

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

            while (true)
            {
                program.serverGetMsg();
                Thread.Sleep(50);
                if(player.process == 1)
                {
                    Console.WriteLine("[DEBUG] ->" + player.warType.ToString() + player.sl_x.ToString() + player.sl_y.ToString());
                    program.SendClientMSG("SLCT_" + player.warType.ToString() + player.sl_x.ToString() + player.sl_y.ToString());
                    player.process = 0;
                }
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
            Program program = new Program();

        }

        public static void RoundCheck(string takenMSG)
        {
            if (takenMSG != "RED_TURN" && takenMSG != "YELLOW_TURN" && player.myTeam == "null")
                return;

            //Console.WriteLine("[DEBUG] -> TAKENMSG IS SUCCESFULLY SEND TO ROUNDCHECK! : " + takenMSG);
            player.PlayerControl(takenMSG);

        }


        public static void InitGame()
        {
            Draw();
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

                        Console.WriteLine("[DEBUG]: -> " + takenMSG);

                        // START GAME MESSAGE
                        if (takenMSG == "all.connected")
                        {
                            Console.Clear();

                            Thread LoginScreen = new Thread(new ThreadStart(LoginThread));

                            LoginScreen.Start();

                            music.PlaySound();

                        }

                        if (isGameStart == false && (takenMSG == "RED" || takenMSG == "YELLOW"))
                        {
                            //ATTACH TEAM MESSAGE
                            
                            initTeam(takenMSG);

                            isGameStart = true;
                        }

                        //PLAY ROUND
                        if((takenMSG == "RED_TURN" || takenMSG == "YELLOW_TURN") && isGameStart == true)
                        {
                            RoundCheck(takenMSG);
                        }

                        // Console.WriteLine("My Team -> " + player.myTeam);

                        if(takenMSG.Contains("DEST"))
                        {
                            CTable.table[Convert.ToInt32(takenMSG.Substring(5, 1)), Convert.ToInt32(takenMSG.Substring(6, 1))] = '■';
                            //CTable.tableColor = CTable.TeamTable;
                            //CTable.Draw();
                            SendClientMSG("CURRENT_TEAM");
                        } else if(takenMSG.Contains("PASS"))
                        {
                            //CTable.tableColor = CTable.TeamTable;
                            //CTable.Draw();
                            SendClientMSG("CURRENT_TEAM");
                        }



                        //
                        break;


                    case Telepathy.EventType.Disconnected:

                        //Console.Clear();
                        Console.WriteLine("Connection Error!");
                        break;
                }
            }
        }

        public void initTeam(string takenMSG)
        {
            if (player.myTeam != "null")
                return;

           // Console.WriteLine("[initTeam] Taken MSG: " + takenMSG);

            if (takenMSG == "RED")
            {
                //Console.WriteLine("\nYou are RED team!");
                player.myTeam = "RED";
                CTable.L1 = "You are RED team!";
                //Program.Draw();
            }
            else if (takenMSG == "YELLOW")
            {
                //Console.WriteLine("\nYou are YELLOW team!");
                player.myTeam = "YELLOW";
                CTable.L1 = "You are YELLOW team!";
                //Program.Draw();
            }

            //Console.Clear();
            //Draw();

                SendClientMSG("CURRENT_TEAM");
        }
        

        public void SendClientMSG(string msg)
        {
            byte[] msgc = Encoding.Default.GetBytes(msg);

            client.Send(msgc);
            //serverGetMsg();
        }

        
    }
}
