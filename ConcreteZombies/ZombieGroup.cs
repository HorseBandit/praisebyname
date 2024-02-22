using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.ConcreteZombies
{
    class ZombieGroup : IZombieComponent
    {
        private List<IZombieComponent> zombies = new List<IZombieComponent>();

        public string Type => "Zombie Group";
        public int Health => zombies.Sum(z => z.Health);

        public void Add(IZombieComponent zombie)
        {
            zombies.Add(zombie);
        }

        public void TakeDamage(int damage)
        {
            if (zombies.Any())
            {
                zombies[0].TakeDamage(damage);

                // Check if the first zombie has died
                if (zombies[0].Health <= 0)
                {
                    //zombies[0].Die(); // Call the Die method for the zombie
                    zombies.RemoveAt(0); // Remove the dead zombie from the group
                    Console.WriteLine($"A zombie has died. {zombies.Count} zombies remaining in the group.");
                }
            }
        }

        public void Die()
        {
            
        }

        public override string ToString()
        {
            var zombieDescriptions = zombies.Select(z => $"- {z.Type} (Health: {z.Health})");
            return $"{Type}:\n{string.Join("\n", zombieDescriptions)}";
        }
    }

}
