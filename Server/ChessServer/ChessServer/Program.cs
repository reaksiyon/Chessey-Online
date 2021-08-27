using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telepathy;

namespace ChessServer
{
    class Program
    {
		//public const bool RED = true;
		//public const bool BLUE = false;

		//public bool[] Team = { RED, BLUE };

		public Server server = new Server();
		public bool GameStarted = false;
		public int def_x, def_y;
		public int att_x, att_y;
		public bool def_selected, att_selected = false;

		/*
		static public int[,] teamTable = new int[,]
{
			  {1, 1, 1, 1, 1}, // 1 red
              {0, 1, 1, 1, 0}, // 2 yellow
              {0, 0, 0, 0, 0},
			  {0, 2, 2, 2, 0},
			  {2, 2, 2, 2, 2},
		};
		*/

		public int RedT = 8;
		public int YellowT = 8;

		static void Main(string[] args)
		{
			Program program = new Program();

			program.server.Start(1337);

			int connectedCount = 1;

			int session = 0;

			int currentTeam = 0;

			bool _switch = true;

			Message msg;

			byte[] wait = Encoding.Default.GetBytes("Please wait for other players..");

			for (; ; )
			{

				if (!program.server.GetNextMessage(out msg))
				{
					Thread.Sleep(1);
				}
				else
				{
					switch (msg.eventType)
					{
						case EventType.Connected:

							connectedCount++;

							session++;

							Console.WriteLine("ConnectedCount:" + connectedCount);

							program.UIDCheck(msg);

							for (int
								i = 0; i <= msg.connectionId; i++)
							{
								program.server.Send(i, wait);
							}

							Console.WriteLine(msg.connectionId.ToString() + " Connected");

							if (session == 2)
							{
								program.StartGame(msg);

								session = 0;
							}
							else
							{
									Console.WriteLine("game is not started because session is : " + session + "/2");
							}

							break;

						case EventType.Data:

							string data = Encoding.ASCII.GetString(msg.data);
							bool switch_team = false;
							Console.WriteLine(data); // message debug
							//for (int i = connectedCount; i < connectedCount; i++)
							//program.server.Send(i, msg.data);

							if(data == "CURRENT_TEAM")
                            {

								if (_switch == true)
								{
									if (currentTeam == 0)
									{
										byte[] message = Encoding.Default.GetBytes("RED_TURN");

										for(int i = 0; i <= msg.connectionId+1; i++)	
										program.server.Send(i, message);

										Console.WriteLine("RED_TURN!");

										currentTeam = 1;
									}
									else if (currentTeam == 1)
									{
										byte[] message = Encoding.Default.GetBytes("YELLOW_TURN");

										for (int i = 0; i <= msg.connectionId+1; i++)
											program.server.Send(i, message);

										Console.WriteLine("YELLOW_TURN!");

										currentTeam = 0;
									}
									
								}

								_switch = !_switch;

							}
							if(data.Contains("SLCT"))
                            {
								/*
								Console.WriteLine("[DEBUG] ->"+ data.Substring(5,1));
								Console.WriteLine("[DEBUG] ->" + data.Substring(6, 1));
								Console.WriteLine("[DEBUG] ->" + data.Substring(7, 1));
								*/
								string wType = data.Substring(5,1);
								string sl_x = data.Substring(6, 1);
								string sl_y = data.Substring(7, 1);

								if(wType == "D")
                                {
									program.def_x = Convert.ToInt32(sl_x);
									program.def_x = Convert.ToInt32(sl_y);

									program.def_selected = true;
								} else {
									program.att_x = Convert.ToInt32(sl_x);
									program.att_x = Convert.ToInt32(sl_y);

									program.att_selected = true;
								}

								if(program.att_selected == true && program.def_selected == true)
                                {
									if(program.att_x != program.def_x || program.att_y != program.def_y)
                                    {
										Console.WriteLine("[DEBUG] -> DESTROY");

										byte[] msgDestroy = Encoding.Default.GetBytes("DEST_" + sl_x + sl_y);

										for (int i = 0; i <= msg.connectionId + 1; i++)
											program.server.Send(i, msgDestroy);

										program.def_selected = false;
										program.att_selected = false;

										if (currentTeam == 0)
											program.RedT -= 1;
										else if (currentTeam == 1)
											program.YellowT -= 1;

										Console.WriteLine("[REDT] => " + program.RedT);
										Console.WriteLine("[YELLOWT] => " + program.YellowT);

									} else
                                    {
										Console.WriteLine("[DEBUG] -> PASS");

										byte[] msgPass = Encoding.Default.GetBytes("PASS");

										for (int i = 0; i <= msg.connectionId + 1; i++)
											program.server.Send(i, msgPass);

										program.def_selected = false;
										program.att_selected = false;

									}
                                }
							}


							break;

						case EventType.Disconnected:
							connectedCount--;

							Console.WriteLine(msg.connectionId.ToString() + " Disconnected");
							Console.WriteLine("ConnectedCount:" + connectedCount);

							break;
					}
				}
			}
		}

		Random rndTeam = new Random();
		public void StartGame(Message msg)
		{
			byte[] msgc = Encoding.Default.GetBytes("all.connected");

			for (int i = 0; i <= msg.connectionId; i++)
				server.Send(i, msgc);

			Console.WriteLine("UID: " + msg.connectionId + " Started to the game!");
			Console.WriteLine("UID: " + (msg.connectionId - 1) + " Started to the game!");

			RollTeam(msg);
			GameStarted = true;
		}

		void UIDCheck(Message msg)
        {

			byte[] uid = Encoding.Default.GetBytes("Your UID: " + msg.connectionId);


			for (int i = 0; i <= msg.connectionId; i++)
			{
				server.Send(i, uid);
			}

		}
		public void RollTeam(Message msg)
        {
			int teamNum = 1;
			int max_val = 3;

			for (int i = 0; i < max_val; i++)
			{
				teamNum = rndTeam.Next(1, max_val);
				teamNum = rndTeam.Next(1, max_val);
			}

			byte[] msgc = Encoding.Default.GetBytes("RED");
			byte[] msgw = Encoding.Default.GetBytes("YELLOW");

			Console.WriteLine("Random team number: " + teamNum);

			if (teamNum == 1)
            {
				server.Send(msg.connectionId, msgc);

				server.Send(msg.connectionId-1, msgw);
			}
			else
            {
				server.Send(msg.connectionId, msgw);

				server.Send(msg.connectionId-1, msgc);
				
			}

		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000213C File Offset: 0x0000033C
		public static byte[] FromHex(string hex)
		{
			hex = hex.Replace("-", "");
			byte[] raw = new byte[hex.Length / 2];
			for (int i = 0; i < raw.Length; i++)
			{
				raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}
			return raw;
		}
	}
    
}
