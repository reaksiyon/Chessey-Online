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
		public const bool RED = true;
		public const bool BLUE = false;

		public bool[] Team = { RED, BLUE };

		public Server server = new Server();
		static void Main(string[] args)
		{
			Program program = new Program();

			

			program.server.Start(1337);

			int connectedCount = 1;

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
							connectedCount += 2;
							for (int UID = 0; UID < connectedCount; UID++)
							{
								program.server.Send(UID, wait);
							}

							Console.WriteLine(msg.connectionId.ToString() + " Connected");

							Console.WriteLine("ConnectedCount:" + connectedCount);

							if (connectedCount > 4)
                            {
								program.StartGame(connectedCount);
                            }

							break;

						case EventType.Data:
							Console.WriteLine(Encoding.ASCII.GetString(msg.data));
							for (int UID = 0; UID < connectedCount; UID++)
							{
								//program.server.Send(UID, msg.data);
							}
							break;

						case EventType.Disconnected:
							connectedCount-=2;
							Console.WriteLine(msg.connectionId.ToString() + " Disconnected");
							break;
					}
				}
			}
		}

		Random rndTeam = new Random();
		public void StartGame(int connectedCount)
        {
			byte[] msgc = Encoding.Default.GetBytes("all.connected");

			for (int UID = 0; UID < connectedCount; UID++)
			{
				server.Send(UID, msgc);
			}

			RollTeam(connectedCount);
		}

		public void RollTeam(int connectedCount)
        {
			int teamNum = 0;
			int max_val = 2;

			for (int i = 0; i < max_val; i++)
			{
				teamNum = rndTeam.Next(0, max_val);
				teamNum = rndTeam.Next(0, max_val);
			}

			byte[] msgc = Encoding.Default.GetBytes("[TURN TEAM]: " + teamNum.ToString());

			for (int UID = 0; UID < connectedCount; UID++)
			{
				server.Send(UID, msgc);
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
