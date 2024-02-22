// See https://aka.ms/new-console-template for more information
using MidtermOneSWE.Factories;
using MidtermOneSWE.Interfaces;

class Program
{
    static List<IZombieComponent> zombies = new List<IZombieComponent>();

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
        IZombieFactory zombieFactory = new ZombieFactory();
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
                    zombies.Add(zombie);
                    Console.WriteLine($"{zombie.Type} Zombie created with {zombie.Health} health.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        } while (input != "z");

        Console.WriteLine("Zombies created:");
        foreach (var zombie in zombies)
        {
            Console.WriteLine($"{zombie.Type} Zombie (Health: {zombie.Health})");
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void DemoGameplay()
    {
        Console.WriteLine("Press the space bar to damage the first zombie, or any other key to return to the main menu.");
        Console.WriteLine("Take a look at your zombies!");
        Console.WriteLine();
        PrintZombies(zombies);
        while (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
        {
            if (zombies.Count > 0)
            {
                zombies[0].TakeDamage(25);
                Console.WriteLine($"{zombies[0].Type} Zombie took 25 damage. Current Health: {zombies[0].Health}");
                Console.WriteLine();

                if (zombies[0].Health <= 0)
                {
                    Console.WriteLine($"{zombies[0].Type} Zombie has died and is removed from the list.");
                    Console.WriteLine();
                    zombies.RemoveAt(0);
                }
            }
            else
            {
                Console.WriteLine("No more zombies left.");
                break;
            }

            PrintZombies(zombies);
            Console.WriteLine("Press the space bar to damage the first zombie, or any other key to return to the main menu.");
        }
    }

    static void PrintZombies(List<IZombieComponent> zombies)
    {
        Console.WriteLine("Current Zombies:");
        foreach (var zombie in zombies)
        {
            Console.WriteLine($"{zombie.Type} Zombie (Health: {zombie.Health})");
        }
    }
}

/*Console.WriteLine("1. Create Zombies?");
Console.WriteLine("2. Demo Gameplay?");
Console.Write("Please select an option: ");

string userchoice = Console.ReadLine();

switch (userchoice)
{
    case "1":
        CreateZombiesPrompt();
        break;
    case "2":
        DemoGameplay();
        break;
    default:
        Console.WriteLine("Invalid selection.");
        break;
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();
        

static void CreateZombiesPrompt()
{
    IZombieFactory zombieFactory = new ZombieFactory();
    List<IZombieComponent> zombies = new List<IZombieComponent>();
    string input;

    do
    {
        Console.WriteLine("Which zombie would you like to create?");
        Console.WriteLine("1. Regular");
        Console.WriteLine("2. Cone");
        Console.WriteLine("3. Bucket");
        Console.WriteLine("4. Screendoor");
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
                zombies.Add(zombie);
                Console.WriteLine($"{zombie.Type} Zombie created with {zombie.Health} health.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

    } while (input != "z");

    Console.WriteLine("Zombies created:");
    foreach (var zombie in zombies)
    {
        Console.WriteLine($"{zombie.Type} Zombie (Health: {zombie.Health})");
    }

    Console.WriteLine("Press any key to exit.");
    Console.ReadKey();
}

static void DemoGameplay()
{
    
}*/
