// See https://aka.ms/new-console-template for more information
using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Factories;
using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
using MidtermOneSWE.Utilities;

class Program
{
    // Define these as static members so they are accessible from static methods like Main and others.
    private static readonly GameObjectManager gameObjectManager;
    private static readonly ZombieFactory zombieFactory;
    private static readonly GameEventManager gameEventManager;

    static Program()
    {
        gameObjectManager = new GameObjectManager();
        zombieFactory = new ZombieFactory();
        zombieFactory.SetGameObjectManager(gameObjectManager); // Now safe, as both instances are surely created

        gameObjectManager.SetZombieFactory(zombieFactory); // Ensure this method exists in GameObjectManager

        gameEventManager = new GameEventManager(gameObjectManager); // Finally, instantiate GameEventManager
    }

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
            Console.WriteLine("Press 'z' to finish creating zombies.");
            Console.Write("Select a type of zombie (1-4): ");

            input = Console.ReadLine();

            if (input == "z") break;

            try
            {
                IZombieComponent zombie = zombieFactory.CreateZombie(input);
                gameObjectManager.AddZombie(zombie);
                Console.WriteLine($"A {zombie.Type} has been created with {zombie.Health} health.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (input != "z");

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void DemoGameplay()
    {
        Console.WriteLine("Take a look at your zombies!");
        PrintAllZombies();

        if (!gameObjectManager.GetAllZombies().Any())
        {
            Console.WriteLine("No zombies available to attack.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\nChoose your plant:");
            Console.WriteLine("1 - Peashooter (25 damage)");
            Console.WriteLine("2 - Watermelon (30 damage)");
            Console.WriteLine("3 - ShroomMagnet (Accessory removal from the first zombie)");
            Console.Write("Enter your choice or press any other key to return to the main menu: ");

            string plantChoice = Console.ReadLine();
            int damage = 0;
            StrikeType selectedStrikeType = StrikeType.Normal;

            switch (plantChoice)
            {
                case "1":
                    damage = 25;
                    selectedStrikeType = StrikeType.Normal;
                    break;
                case "2":
                    damage = 30;
                    selectedStrikeType = StrikeType.WatermelonOverhead;
                    break;
                case "3":
                    selectedStrikeType = StrikeType.MushroomExtract;
                    break;
                default:
                    return;
            }

            Console.WriteLine("Press the space bar to launch the attack.");
            Console.WriteLine();
            if (Console.ReadKey(true).Key != ConsoleKey.Spacebar) return;

            var firstZombie = gameObjectManager.GetAllZombies().FirstOrDefault();
            if (firstZombie != null)
            {
                var attackOutcome = firstZombie.TakeDamage(damage, selectedStrikeType);

                if (selectedStrikeType == StrikeType.MushroomExtract)
                {
                    Console.WriteLine("Magnet Shroom used on the first zombie. Accessory removed if applicable.");
                }
                else if (attackOutcome.outcome == DmgOutcome.DamageDealt)
                {
                    Console.WriteLine($"{firstZombie.Type} Zombie took {damage} damage. Current Health: {Math.Max(0, firstZombie.Health)}");
                }
                else if (attackOutcome.outcome == DmgOutcome.AccessoryRemoved)
                {
                    Console.WriteLine($"{firstZombie.Type} Zombie's accessory protected it from damage. No damage was taken.");
                }
                else
                {
                    Console.WriteLine($"The attack had no effect on {firstZombie.Type} Zombie.");
                }

                if (firstZombie.Health <= 0)
                {
                    Console.WriteLine($"{firstZombie.Type} Zombie has died and is removed from the list.");
                    gameObjectManager.RemoveZombie(firstZombie);
                }

                PrintAllZombies();
            }
            else
            {
                Console.WriteLine("No more zombies to attack.");
                break;
            }
        }
    }

    static void PrintAllZombies()
    {
        Console.WriteLine("Current Zombies:");
        foreach (var zombie in gameObjectManager.GetAllZombies())
        {
            Console.WriteLine($"{zombie.Type} Zombie (Health: {zombie.Health})");
        }
    }

    static void Cleanup()
    {
        Console.WriteLine("Cleaning up before exit...");

    }
}