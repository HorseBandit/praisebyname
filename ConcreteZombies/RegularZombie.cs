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
        }

        // Constructor
        public RegularZombie() { }
        public (bool success, DmgOutcome outcome) TakeDamage(int damage, StrikeType strikeType)
        {
            // If the strike type is something that RegularZombie should be immune to, adjust logic here
            // For now, assuming all damage is taken as usual

            if (damage <= 0)
            {
                return (false, DmgOutcome.NoEffect); // No damage taken, no effect
            }

            Health -= damage;
            if (Health < 0)
            {
                Health = 0; // Ensure health does not drop below zero
            }

            if (Health == 0)
            {
                Die(); // Trigger the death process
                return (true, DmgOutcome.DamageDealt); // Damage led to zombie's demise
            }

            return (true, DmgOutcome.DamageDealt); // Damage was successfully dealt
        }

        public void Die()
        {
            Console.WriteLine($"{Type} Zombie has died!");
        }

        // Utility method to handle the transformation process
       /* protected void TransformToRegular()
        {
            Console.WriteLine($"{Type} Zombie is transforming into a Regular Zombie.");
            OnTransformation?.Invoke(this, new RegularZombie());
        }*/
    }
}
