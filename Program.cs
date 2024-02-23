// See https://aka.ms/new-console-template for more information
using MidtermOneSWE.ConcreteZombies;
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
            Console.WriteLine("5. ZombieGroup");
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
                    "5" => zombieFactory.CreateZombie("Group"),
                    "z" => null,
                    _ => throw new ArgumentException("Invalid zombie type", nameof(input)),
                };

                if (zombie != null)
                {
                    if (zombie is ConeZombie coneZombie)
                    {
                        coneZombie.OnTransformation += HandleZombieTransformation;
                    }
                    if (zombie is BucketZombie bucketZombie)
                    {
                        bucketZombie.OnTransformation += HandleZombieTransformation;
                    }
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
        Console.WriteLine();
        Console.WriteLine("Take a look at your zombies!");
        Console.WriteLine();
        PrintZombies(zombies);
        Console.WriteLine();
        Console.Write("Enter damage value: ");
        Console.WriteLine();
        string damageInput = Console.ReadLine();

        if (damageInput.ToLower() == "exit")
        {
            return;
        }

        if (!int.TryParse(damageInput, out int damage) || damage <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for damage.");
            return;
        }

        Console.WriteLine($"Damage set to {damage}. Press the space bar to damage the first zombie. Other keys return to main menu.");
        Console.WriteLine();

        while (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
        {
            if (zombies.Count > 0)
            {
                zombies[0].TakeDamage(damage);
                Console.WriteLine($"{zombies[0].Type} Zombie took {damage} damage. Current Health: {zombies[0].Health}");

                if (zombies[0].Health <= 0)
                {
                    Console.WriteLine($"{zombies[0].Type} Zombie has died and is removed from the list.");
                    zombies.RemoveAt(0);
                }

                if (zombies.Count > 0)
                {
                    PrintZombies(zombies);
                    Console.WriteLine("Press the space bar to damage the first zombie. Other keys return to main menu.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Zombies eradicated.");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Zombies eradicated.");
                break;
            }
        }
    }

    static void PrintZombies(List<IZombieComponent> zombies)
    {
        Console.WriteLine("Current Zombies:");
        foreach (var zombie in zombies)
        {
            Console.WriteLine(zombie);
        }
    }

    static void HandleZombieTransformation(IZombieComponent oldZombie, IZombieComponent newZombie)
    {
        int index = zombies.IndexOf(oldZombie);
        if (index != -1)
        {
            zombies[index] = newZombie;
            Console.WriteLine($"A {oldZombie.Type} Zombie transformed into a Regular Zombie!");
        }
    }
}