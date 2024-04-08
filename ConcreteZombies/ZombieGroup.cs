using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.ConcreteZombies
{
    /// <summary>
    /// Class for ZombieGroups using the Composite pattern.
    /// </summary>
    class ZombieGroup : IZombieComponent
    {
        private List<IZombieComponent> zombies = new List<IZombieComponent>();

        public string Type => "Zombie Group";
        public int Health => zombies.Sum(z => z.Health);

        public void Add(IZombieComponent zombie)
        {
            zombies.Add(zombie);
        }

        /// <summary>
        /// Method for allowing the group of zombies to take damage.
        /// </summary>
        /// <param name="damage"></param>
        public bool TakeDamage(int damage)
        {

            if (zombies.Any())
            {
                var firstZombie = zombies[0];
                firstZombie.TakeDamage(damage);

                if (firstZombie.Health <= 0)
                {
                    
                    firstZombie.Die();
                    zombies.RemoveAt(0);
                    return true;
                }
                else if ((firstZombie is ConeZombie || firstZombie is BucketZombie || firstZombie is ScreendoorZombie) && firstZombie.Health <= 75)
                {
                    var transformedZombie = new RegularZombie();
                    transformedZombie.SetHealth(firstZombie.Health);
                    zombies[0] = transformedZombie; 

                    Console.WriteLine($"{firstZombie.Type} Zombie lost its accessory and became a Regular Zombie with {transformedZombie.Health} health!");
                    return false;
                }
            }
            return false;
        }

        public void Die()
        {
            
        }

        /// <summary>
        /// Overridden ToString to print zombie descriptions belonging to zombiegroups.
        /// </summary>
        /// <returns>
        /// Overridden ToString.
        ///</returns>
        public override string ToString()
        {
            var zombieDescriptions = zombies.Select(z => $"- {z.Type} (Health: {z.Health})");
            return $"{Type}:\n{string.Join("\n", zombieDescriptions)}";
        }
    }
}
