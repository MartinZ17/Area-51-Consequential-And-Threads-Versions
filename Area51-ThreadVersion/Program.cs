using Area51_ThreadVersion;
using System.Threading.Channels;

namespace Area51_ThreadVersion
{
	internal class Program
	{
		static (string Name, string SecLevel) Agent;
		
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Area 51");
			Console.WriteLine("Press ENTER to start the simulation.");
			Console.ReadLine();

			Random rnd = new Random();
			int currentFloor = 1;
            Console.WriteLine("Current floor: " + currentFloor);
            for (int i = 0; i < rnd.Next(1, 20); i++)
			{
				Console.WriteLine("New agent enter the elevator");
				Console.WriteLine();
				
				Thread agentThread = new Thread(() => MakeAgent());
				agentThread.Start();
				agentThread.Join();

				Thread elevatorThread = new Thread(() => Elevator(Agent.SecLevel, currentFloor));
				elevatorThread.Start();
				elevatorThread.Join();
				
				Console.WriteLine("#########################################");
			}

			Console.WriteLine("Base is closed! Elevator stop working.");
		}

		public static void MakeAgent()
		{
			Entities entities = new Entities();
			var rand = new Random();
			Agent.Name = entities.Names[rand.Next(entities.Names.Count)];
			Agent.SecLevel = entities.SecurityLevels[rand.Next(entities.SecurityLevels.Count)];
			Console.WriteLine("Agent name: " + Agent.Name);
			Console.WriteLine("Sec. Level: " + Agent.SecLevel);
		}

		public static bool SecurityCheck(string secLevel, int floor)
		{
			if (secLevel == "Confidential" && floor == 1)
			{
				Console.WriteLine();
				Console.WriteLine("Security check passed. The door is opening and agent leave the elevator.");
				Console.WriteLine();
				return true;
			}

			if (secLevel == "Secret" && (floor == 1 || floor == 2))
			{
				Console.WriteLine();
				Console.WriteLine("Security check passed. The door is opening and agent leave the elevator.");
				Console.WriteLine();
				return true;
			}

			if (secLevel == "Top-secret")
			{
				Console.WriteLine();
				Console.WriteLine("Security check passed. The door is opening and agent leave the elevator.");
				Console.WriteLine();
				return true;
			}

			Console.WriteLine();
			Console.WriteLine("Security check failed. Choose another floor: ");
			return false;
		}

		public static void Elevator(string secLevel, int currentFloor)
		{
			int selectedFloor = 0;
			do
			{
				Random rnd = new Random();
				Console.WriteLine("Choose floor: G[1], S[2], T1[3], T2[4]");
				selectedFloor = rnd.Next(1, 5);
				Console.Write($"Going to floor {selectedFloor} ");
				for (int j = 0; j < Math.Abs(selectedFloor - currentFloor); j++)
				{
					Console.Write(".");
					Thread.Sleep(1000);
				}

				Console.WriteLine();
				currentFloor = selectedFloor;
				Console.WriteLine("Current floor: " + currentFloor);
			} while (SecurityCheck(secLevel, selectedFloor) == false);
		}
	}
}