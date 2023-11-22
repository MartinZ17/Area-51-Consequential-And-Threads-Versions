using Area51;
using System.Threading.Channels;

namespace Area51
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Area 51");
			Console.WriteLine("Press ENTER to start the simulation.");
			Console.ReadLine();

			Random rnd = new Random();
			int selectedFloor = 0;
			int currentFloor = 1;
			for (int i = 0; i < rnd.Next(1, 15); i++)
			{
				Agent agent = MakeAgent();
                Console.WriteLine("New agent enter the elevator");
                Console.WriteLine("Agent name: " + agent.Name);
				Console.WriteLine("Sec. Level: " + agent.SecurityLevel);
                Console.WriteLine();
				
				do
				{
					Console.WriteLine("Choose floor: G[1], S[2], T1[3], T2[4]");
					selectedFloor = rnd.Next(1, 4); //int.Parse(Console.ReadLine());
                    Console.Write($"Going to floor {selectedFloor} ");
                    for (int j = 0; j < Math.Abs(selectedFloor - currentFloor); j++)
					{
                        Console.Write(".");
                        Thread.Sleep(1000);
					}

                    Console.WriteLine();
					currentFloor = selectedFloor;
					Console.WriteLine("Current floor: " + currentFloor);
				} while (SecurityCheck(agent, selectedFloor) == false);

				
                Console.WriteLine("#########################################");
				Console.WriteLine("Current floor: " + currentFloor);
			}

            Console.WriteLine("Base is closed! Elevator stop working.");
        }

		public static Agent MakeAgent()
		{
			Agent agent = new Agent();
			Entities entities = new Entities();
			var rand = new Random();
			agent.Name = entities.Names[rand.Next(entities.Names.Count)];
			agent.SecurityLevel = entities.SecurityLevels[rand.Next(entities.SecurityLevels.Count)];

			return agent; 
		}

		public static bool SecurityCheck(Agent agent, int floor)
		{
			if(agent.SecurityLevel == "Confidential" && floor == 1 )
			{
				Console.WriteLine();
				Console.WriteLine("Security check passed. The door is opening and agent leave the elevator.");
				Console.WriteLine();
				return true;
			}

            if (agent.SecurityLevel == "Secret" && (floor == 1 || floor == 2))
			{
                Console.WriteLine();
                Console.WriteLine("Security check passed. The door is opening and agent leave the elevator.");
				Console.WriteLine();
				return true;
			}

			if (agent.SecurityLevel == "Top-secret")
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
	}
}