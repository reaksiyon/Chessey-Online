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
		static void Main(string[] args)
		{
			Program program = new Program();

			program.server.Start(1337);

			int connectedCount = 1;

			int session = 0;


			

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

							for (int i = 0; i < msg.connectionId; i++)
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

							Console.WriteLine(Encoding.ASCII.GetString(msg.data));

							//for (int i = connectedCount; i < connectedCount; i++)
							//program.server.Send(i, msg.data);
							if(program.GameStarted == true)
                            {
								byte[] message = Encoding.Default.GetBytes(Encoding.ASCII.GetString(msg.data));
								program.server.Send(msg.connectionId, message);

								program.server.Send(msg.connectionId - 1, message);
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

			server.Send(msg.connectionId, msgc);
			server.Send(msg.connectionId - 1, msgc);

			Console.WriteLine("UID: " + msg.connectionId + " Started to the game!");
			Console.WriteLine("UID: " + (msg.connectionId - 1) + " Started to the game!");

			RollTeam(msg);
			GameStarted = true;
		}

		void UIDCheck(Message msg)
        {

			byte[] uid = Encoding.Default.GetBytes("Your UID: " + msg.connectionId);

				server.Send(msg.connectionId, uid);

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

			byte[] msgc = Encoding.Default.GetBytes("\nYOUR TURN: ");
			byte[] msgw = Encoding.Default.GetBytes("\nWaiting for opponent turn...");

			Console.WriteLine("Random team number: " + teamNum);

			if (teamNum == 1)
            {
				server.Send(msg.connectionId, msgc);

				server.Send(msg.connectionId-1, msgw);
			}
			else
            {
				server.Send(msg.connectionId-1, msgc);

				server.Send(msg.connectionId, msgw);
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
