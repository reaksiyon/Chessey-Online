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


		static void Main(string[] args)
		{
			Program program = new Program();

			program.server.Start(1337);

			int connectedCount = 1;

			int session = 0;

			byte[] wait = Encoding.Default.GetBytes("Please wait for other players..");

			for (; ; )
			{
				Message msg;

				if (!program.server.GetNextMessage(out msg))
				{
					Thread.Sleep(1);
				}
				else
				{
					switch (msg.eventType)
					{
						case EventType.Connected:

							connectedCount+=2;

							session++;

							Console.WriteLine("ConnectedCount:" + connectedCount);

							program.UIDCheck(connectedCount);
							
							for (int i = 0; i < connectedCount; i++)
							{
								program.server.Send(i, wait);
							}

							Console.WriteLine(msg.connectionId.ToString() + " Connected");

							if (connectedCount > 4 && session == 2)
                            {
								program.StartGame(connectedCount);

								session = 0;
                            }
							else
                            {
								if(connectedCount <= 4)
								Console.WriteLine("game is not started because connectedCount is : " + connectedCount + "/5");
								
								if(session!=2)
								Console.WriteLine("game is not started because session is : " + session + "/2");
							}

							break;

						case EventType.Data:

							Console.WriteLine(Encoding.ASCII.GetString(msg.data));

							//for (int i = connectedCount; i < connectedCount; i++)
								//program.server.Send(i, msg.data);

							break;

						case EventType.Disconnected:
							connectedCount -= 2;

							Console.WriteLine(msg.connectionId.ToString() + " Disconnected");
							Console.WriteLine("ConnectedCount:" + connectedCount);
							
							break;
					}
				}
			}
		}

		Random rndTeam = new Random();
		public void StartGame(int connectedCount)
        {
			byte[] msgc = Encoding.Default.GetBytes("all.connected");

			for (int i = 1; i <= connectedCount; i++)
			{
				server.Send(i, msgc);
				Console.WriteLine("UID: " + i + " Started to the game!");
			}

			RollTeam(connectedCount);
		}

		void UIDCheck(int connectedCount)
        {
			byte[] uid = Encoding.Default.GetBytes("Your UID: " + connectedCount);

			for (int i = 0; i <= connectedCount+2; i++)
			{
				server.Send(i, uid);
			}
		}
		public void RollTeam(int connectedCount)
        {
			int teamNum = 1;
			int max_val = 3;

			for (int i = 0; i < max_val; i++)
			{
				teamNum = rndTeam.Next(1, max_val);
				teamNum = rndTeam.Next(1, max_val);
			}

			byte[] msgc = Encoding.Default.GetBytes("YOUR TURN: ");
			byte[] msgw = Encoding.Default.GetBytes("Waiting for opponent turn...");

			if (teamNum == 1)
            {
				server.Send(connectedCount-4, msgc);

				server.Send(connectedCount-3, msgw);
			}
			else
            {
				server.Send(connectedCount-3, msgc);

				server.Send(connectedCount-4, msgw);
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
