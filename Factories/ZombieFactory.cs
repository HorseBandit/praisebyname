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
    class ZombieFactory : IZombieFactory
    {
        private Random random = new Random();

        private GameObjectManager _gameObjectManager;

        // Constructor to set GameObjectManager
        public ZombieFactory(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
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
        public IZombieComponent CreateZombie(string type)
        {
            IZombieComponent baseZombie = new RegularZombie(); // Base zombie for decorators
            switch (type)
            {
                case "Regular":
                    return baseZombie;
                case "Cone":
                    return new ConeDecorator(baseZombie, this, _gameObjectManager, 75);
                case "Bucket":
                    return new BucketDecorator(baseZombie, this, _gameObjectManager, 150);
                case "Screendoor":
                    return new ScreenDoorDecorator(baseZombie, this, _gameObjectManager, 75);
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
