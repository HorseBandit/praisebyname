using MidtermOneSWE.Factories;
using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MidtermOneSWE.Interfaces.IZombieComponent;

namespace MidtermOneSWE.ConcreteZombies
{
    /// <summary>
    /// Classes for all four types of zombies of type IZombieComponent. Documentation for regular zombies below, others ommitted.
    /// </summary>
    class RegularZombie : IZombieComponent
    {
        public string Type => "Regular";
        public int Health { get; private set; } = 50;

        /// <summary>
        /// SetHealth method to maintain zombie health after transforming.
        /// </summary>
        /// <param name="newHealth"></param>
        public void SetHealth(int newHealth)
        {
            Health = newHealth;
        }


        /// <summary>
        /// Damage method with health set at lower bound zero.
        /// </summary>
        /// <param name="damage"></param>
        public bool TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            return true;
        }

        /// <summary>
        /// Overridden ToString method to print zombie type and health.
        /// </summary>
        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine();
            Console.WriteLine("DIE METHOD! Regular Zombie has died!");
            Console.WriteLine();
        }
    }

    class ConeZombie : IZombieComponent
    {

        public event ZombieTransformationHandler OnTransformation;

        private readonly IZombieFactory _zombieFactory;

        public ConeZombie(IZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
        }
        public string Type => "Cone";
        public int Health { get; private set; } = 75;

        public bool HasAccessory { get; private set; } = true;

        public bool TakeDamage(int damage)
        {
            if (HasAccessory)
            {
                KnockAccessory();
                return false; // Indicates no damage was taken, only accessory knocked off
            }

            // Damage application logic remains the same
            Health -= damage;
            //CheckHealthAndTransform();
            return true; // Indicates damage was taken
        }

        /*public void TakeDamage(int damage)
        {
            // Check if the zombie has an accessory before applying damage
            if (HasAccessory)
            {
                KnockAccessory();
                // Return early to ensure no damage is taken while the accessory is present
                return;
            }

            // Apply damage if there is no accessory
            Health -= damage;
            //TransformAccessoryZombieToRegular();
        }*/

        public void KnockAccessory()
        {
            HasAccessory = false;
            Console.WriteLine($"{Type} Zombie's accessory knocked off!");
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            OnTransformation?.Invoke(this, regularZombie);
        }

        /*public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            else if (Health <= 75)
            {
                TransformAccessoryZombieToRegular();
            }
        }*/

        /// <summary>
        /// TransformToRegular method to convert an accessory zombie to a regular zombie.
        /// </summary>
        private void TransformAccessoryZombieToRegular()
        {
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            Console.WriteLine("Cone Zombie lost its cone and became a Regular Zombie!");
            OnTransformation?.Invoke(this, regularZombie);
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine();
            Console.WriteLine("DIE METHOD! Cone Zombie has died!");
            Console.WriteLine();
        }
    }

    class BucketZombie : IZombieComponent
    {
        private readonly IZombieFactory _zombieFactory;

        public event ZombieTransformationHandler OnTransformation;

        public BucketZombie(IZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
        }
        public string Type => "Bucket";
        public int Health { get; private set; } = 150;

        public bool HasAccessory { get; private set; } = true;

        public bool TakeDamage(int damage)
        {
            // Check if the zombie has an accessory before applying damage
            if (HasAccessory)
            {
                KnockAccessory();
                // Return early to ensure no damage is taken while the accessory is present
                return false;
            }

            // Apply damage if there is no accessory
            Health -= damage;
            return true;
            //TransformAccessoryZombieToRegular();
        }

        public void KnockAccessory()
        {
            HasAccessory = false;
            Console.WriteLine($"{Type} Zombie's accessory knocked off!");
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            OnTransformation?.Invoke(this, regularZombie);
        }

        /*public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            else if (Health <= 75)
            {
                TransformAccessoryZombieToRegular();
            }
        }*/

        private void TransformAccessoryZombieToRegular()
        {
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            Console.WriteLine("WHOA! Flying bucket! It's now a Regular Zombie!");
            OnTransformation?.Invoke(this, regularZombie);
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine();
            Console.WriteLine("DIE METHOD! Bucket Zombie has died!");
            Console.WriteLine();
        }
    }

    class ScreendoorZombie : IZombieComponent
    {
        private readonly IZombieFactory _zombieFactory;

        public event ZombieTransformationHandler OnTransformation;
        public string Type => "Screendoor";
        public int Health { get; private set; } = 75;

        public ScreendoorZombie(IZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
        }

        public bool HasAccessory { get; private set; } = true;

        public bool TakeDamage(int damage)
        {
            // Check if the zombie has an accessory before applying damage
            if (HasAccessory)
            {
                KnockAccessory();
                // Return early to ensure no damage is taken while the accessory is present
                return false;
            }

            // Apply damage if there is no accessory
            Health -= damage;
            return true;
            //TransformAccessoryZombieToRegular();
        }

        public void KnockAccessory()
        {
            HasAccessory = false;
            Console.WriteLine($"{Type} Zombie's accessory knocked off!");
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            OnTransformation?.Invoke(this, regularZombie);
        }

        /*public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            else if (Health <= 75)
            {
                TransformAccessoryZombieToRegular();
            }
        }*/

        private void TransformAccessoryZombieToRegular()
        {
            var regularZombie = (RegularZombie)_zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            Console.WriteLine("SAILING SCREENDOOR! That's now a Regular Zombie!");
            OnTransformation?.Invoke(this, regularZombie);
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine();
            Console.WriteLine("DIE METHOD! Screendoor Zombie has died!");
            Console.WriteLine();
        }
    }
}
