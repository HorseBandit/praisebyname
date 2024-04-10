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
            // Your logic to create different types of zombies
            // This method might utilize gameObjectManager for certain operations, if needed

            switch (zombieType)
            {
                case "Regular":
                    return new RegularZombie();
                // Handle other types similarly
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
