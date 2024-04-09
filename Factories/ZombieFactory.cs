using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Decorators;
using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Factories
{
    /// <summary>
    /// ZombieFactory implements IZombieFactory
    /// </summary>
    class ZombieFactory : IZombieFactory
    {
        private Random random = new Random();

        /// <summary>
        /// CreateZombie single method for creating all types of zombies using the zombie factory.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>
        /// A zombie of type IZombieComponent
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// 
        public IZombieComponent CreateZombie(string type)
        {
            switch (type)
            {
                case "Regular":
                    return new RegularZombie(); // RegularZombie constructor sets health to 50 by default.
                case "Cone":
                    return new ConeDecorator(new RegularZombie(), this, 75); // Pass health value to the constructor.
                case "Bucket":
                    return new BucketDecorator(new RegularZombie(), this, 150); // Pass health value to the constructor.
                case "Screendoor":
                    return new ScreendoorDecorator(new RegularZombie(), this, 75); // Pass health value to the constructor.
                default:
                    throw new ArgumentException("Invalid zombie type", nameof(type));
            }
        }

        /// <summary>
        /// CreateRandomZombie creates a random zombie of four given types.
        /// </summary>
        /// <returns>
        /// A random zombie of type IZombieComponent
        /// </returns>
        private IZombieComponent CreateRandomZombie()
        {
            string[] types = new string[] { "Regular", "Cone", "Bucket", "Screendoor" };
            int index = random.Next(types.Length);
            return CreateZombie(types[index]);
        }
    }
}
