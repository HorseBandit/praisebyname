using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MidtermOneSWE.Interfaces.IZombieComponent;

namespace MidtermOneSWE.ConcreteZombies
{
    class RegularZombie : IZombieComponent
    {
        public event ZombieTransformationHandler OnTransformation;

        public string Type { get; private set; } = "Regular";
        public int Health { get; private set; } = 50;
        public bool HasAccessory { get; private set; } = false;
        public bool HasMetal { get; private set; } = false;

        public void SetHealth(int newHealth)
        {
            Health = newHealth;
            // Additional logic can be added here if needed, such as checking for zombie death
        }

        // Constructor
        public RegularZombie() { }

        public bool TakeDamage(int damage, StrikeType strikeType)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            return true; // Indicates damage was taken
        }

        public void Die()
        {
            Console.WriteLine($"{Type} Zombie has died!");
        }

        // Utility method to handle the transformation process
        protected void TransformToRegular()
        {
            // Example transformation logic - can be customized
            // This is just a placeholder to show where and how you might handle transformation
            // In reality, transformation logic might involve notifying some manager or factory to replace the object
            Console.WriteLine($"{Type} Zombie is transforming into a Regular Zombie.");
            OnTransformation?.Invoke(this, new RegularZombie()); // Creates a new RegularZombie instance
        }
    }
}
