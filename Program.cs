// See https://aka.ms/new-console-template for more information
using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Factories;
using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
using MidtermOneSWE.Utilities;

class Program
{
    private static GameObjectManager gameObjectManager = new GameObjectManager();
    private static ZombieFactory zombieFactory = new ZombieFactory(gameObjectManager);
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
        IZombieFactory zombieFactory = new ZombieFactory(gameObjectManager);

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
                IZombieComponent zombie = null;
                string zombieType = "";
                switch (input)
                {
                    case "1":
                        zombie = zombieFactory.CreateZombie("Regular");
                        zombieType = "Regular";
                        break;
                    case "2":
                        zombie = zombieFactory.CreateZombie("Cone");
                        zombieType = "Cone";
                        break;
                    case "3":
                        zombie = zombieFactory.CreateZombie("Bucket");
                        zombieType = "Bucket";
                        break;
                    case "4":
                        zombie = zombieFactory.CreateZombie("Screendoor");
                        zombieType = "Screendoor";
                        break;
                    case "z":
                        // Exit the loop
                        break;
                    default:
                        throw new ArgumentException("Invalid zombie type", nameof(input));
                }

                if (zombie != null)
                {
                    gameObjectManager.AddZombie(zombie); // Assuming gameObjectManager is accessible here
                    Console.WriteLine($"{zombieType} Zombie created with {zombie.Health} health.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (input != "z");

        Console.WriteLine("Zombies created:");
        PrintAllZombies(gameObjectManager);

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void DemoGameplay()
    {
        Console.WriteLine("Take a look at your zombies!");
        Console.WriteLine();
        PrintAllZombies(gameObjectManager);

        if (!gameObjectManager.GetAllZombies().Any())
        {
            Console.WriteLine("No zombies available to attack.");
            return;
        }

        while (true)
        {
            Console.WriteLine("Choose your plant:");
            Console.WriteLine("1 - Peashooter (25 damage)");
            Console.WriteLine("2 - Watermelon (30 damage)");
            Console.WriteLine("3 - ShroomMagnet (Special functionality)");
            Console.WriteLine("Press any other key to return to the main menu.");
            Console.Write("Enter your choice: ");

            string plantChoice = Console.ReadLine();
            StrikeType selectedStrikeType = StrikeType.Normal; // Default to Normal
            int damage = 0;

            switch (plantChoice)
            {
                case "1":
                    damage = 25;
                    selectedStrikeType = StrikeType.Normal;
                    Console.WriteLine("Peashooter selected. Press the space bar to attack.");
                    break;
                case "2":
                    damage = 30;
                    selectedStrikeType = StrikeType.WatermelonOverhead;
                    Console.WriteLine("Watermelon selected. Press the space bar to attack.");
                    break;
                case "3":
                    damage = 0; // Assuming ShroomMagnet has a special effect, adjust as needed
                    selectedStrikeType = StrikeType.MushroomExtract;
                    Console.WriteLine("ShroomMagnet selected. Press the space bar to attack.");
                    break;
                default:
                    return; // Exit the gameplay loop if any other key is pressed
            }

            if (Console.ReadKey(true).Key != ConsoleKey.Spacebar) return; // Exit if the spacebar isn't pressed

            var firstZombie = gameObjectManager.GetAllZombies().FirstOrDefault();
            if (firstZombie != null)
            {
                bool damageTaken = firstZombie.TakeDamage(damage, selectedStrikeType);
                if (damageTaken)
                {
                    Console.WriteLine($"{firstZombie.Type} Zombie took {damage} damage. Current Health: {firstZombie.Health}");
                }

                if (firstZombie.Health <= 0)
                {
                    Console.WriteLine($"{firstZombie.Type} Zombie has died and is removed from the list.");
                    gameObjectManager.RemoveZombie(firstZombie);
                }

                PrintAllZombies(gameObjectManager); // Print the list of zombies after the attack
            }
            else
            {
                Console.WriteLine("No more zombies to attack.");
                break; // Exit the loop if no zombies are left
            }
        }
    }

    static void PrintAllZombies(GameObjectManager gameObjectManager)
    {
        Console.WriteLine("Current Zombies:");
        foreach (var zombie in gameObjectManager.GetAllZombies())
        {
            Console.WriteLine($"{zombie.Type} Zombie (Health: {zombie.Health})");
        }
    }
}