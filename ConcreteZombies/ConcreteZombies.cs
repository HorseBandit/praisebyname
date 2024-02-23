using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.ConcreteZombies
{
    class RegularZombie : IZombieComponent
    {
        public string Type => "Regular";
        public int Health { get; private set; } = 50;

        public void SetHealth(int newHealth)
        {
            Health = newHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine("DIE METHOD! Regular Zombie has died!");
        }
    }

    class ConeZombie : IZombieComponent
    {
        public string Type => "Cone";
        public int Health { get; private set; } = 75;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine("DIE METHOD! Cone Zombie has died!");
        }
    }

    class BucketZombie : IZombieComponent
    {
        public string Type => "Bucket";
        public int Health { get; private set; } = 150;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine("DIE METHOD! Bucket Zombie has died!");
        }
    }

    class ScreendoorZombie : IZombieComponent
    {
        public string Type => "Screendoor";
        public int Health { get; private set; } = 75;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public override string ToString()
        {
            return $"{Type} Zombie (Health: {Health})";
        }

        public void Die()
        {
            Console.WriteLine("DIE METHOD! Screendoor Zombie has died!");
        }
    }

}
