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

        public event ZombieTransformationHandler OnTransformation;

        private readonly IZombieFactory _zombieFactory;

        public ConeZombie(IZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
        }
        public string Type => "Cone";
        public int Health { get; private set; } = 75;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
            else if (Health <= 75)
            {
                TransformToRegular();
            }
        }

        private void TransformToRegular()
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
