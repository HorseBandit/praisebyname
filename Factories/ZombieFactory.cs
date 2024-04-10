using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Decorators;
using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
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
    public class ZombieFactory : IZombieFactory
    {
        private Random random = new Random();

        private GameObjectManager gameObjectManager;

        // Default constructor if no initial setup is required beyond the GameObjectManager
        public ZombieFactory()
        {
        }

        public void SetGameObjectManager(GameObjectManager manager)
        {
            gameObjectManager = manager;
        }

        /// <summary>
        /// CreateZombie single method for creating all types of zombies using the zombie factory.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>
        /// A zombie of type IZombieComponent
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// 
        public IZombieComponent CreateZombie(string zombieType)
        {
            // Base component
            IZombieComponent baseZombie = new RegularZombie(); // Assumes a default constructor is available

            switch (zombieType)
            {
                case "Regular":
                    return baseZombie;
                case "Cone":
                    // Wrap the base zombie in a ConeDecorator
                    return new ConeDecorator(baseZombie, this, gameObjectManager, 75); // Health value is exemplary
                case "Bucket":
                // Similar wrapping in a BucketDecorator
                    return new BucketDecorator(baseZombie, this, gameObjectManager, 150);
                    case "Screendoor":
                // Similar wrapping in a ScreendoorDecorator
                return new ScreenDoorDecorator(baseZombie, this, gameObjectManager, 75);
                default:
                    throw new ArgumentException("Unsupported zombie type", nameof(zombieType));
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
