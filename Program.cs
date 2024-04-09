// See https://aka.ms/new-console-template for more information
using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Factories;
using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
using MidtermOneSWE.Utilities;

class Program
{
    private static GameObjectManager gameObjectManager = new GameObjectManager();
    private static ZombieFactory zombieFactory = new ZombieFactory();
    private static GameEventManager gameEventManager = new GameEventManager(gameObjectManager);

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("1. Create Zombies?");
            Console.WriteLine("2. Demo Gameplay?");
            Console.WriteLine("3. Exit");
            Console.Write("Please select an option: ");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    CreateZombiesPrompt();
                    break;
                case "2":
                    DemoGameplay();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }

    static void CreateZombiesPrompt()
    {
        string input;

        do
        {
            Console.WriteLine("Which zombie would you like to create?");
            Console.WriteLine("1. Regular");
            Console.WriteLine("2. Cone");
            Console.WriteLine("3. Bucket");
            Console.WriteLine("4. Screendoor");
            Console.WriteLine();
            Console.WriteLine("Press 'z' to finish creating zombies.");
            Console.Write("Select a type of zombie: ");

            input = Console.ReadLine();

            try
            {
                IZombieComponent zombie = input switch
                {
                    "1" => zombieFactory.CreateZombie("Regular"),
                    "2" => zombieFactory.CreateZombie("Cone"),
                    "3" => zombieFactory.CreateZombie("Bucket"),
                    "4" => zombieFactory.CreateZombie("Screendoor"),
                    "z" => null,
                    _ => throw new ArgumentException("Invalid zombie type", nameof(input)),
                };

                if (zombie != null)
                {
                    gameObjectManager.AddZombie(zombie);
                    Console.WriteLine($"{zombie.Type} Zombie created with {zombie.Health} health.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        } while (input != "z");

        Console.WriteLine("Zombies created:");
        foreach (var zombie in gameObjectManager.GetAllZombies())
        {
            Console.WriteLine(zombie);
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void DemoGameplay()
    {
        Console.WriteLine("Press the space bar to simulate an attack on the first zombie, or any other key to return to the main menu.");
        Console.WriteLine();
        Console.WriteLine("Take a look at your zombies!");
        Console.WriteLine();
        Console.WriteLine("Choose your plant:");
        Console.WriteLine("1 - Peashooter (25 damage)");
        Console.WriteLine("2 - Watermelon (30 damage)");
        Console.WriteLine("3 - ShroomMagnet (Special functionality)");
        Console.Write("Enter your choice: ");

        string plantChoice = Console.ReadLine();
        int selectedPlantType = int.Parse(plantChoice);

        // Simulating the attack without directly interacting with zombies here
        gameEventManager.SimulateCollisionDetection(selectedPlantType);

        Console.WriteLine("Simulation complete. Press any key to return to the main menu.");
        Console.ReadKey();
    }
}