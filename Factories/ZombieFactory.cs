﻿using MidtermOneSWE.ConcreteZombies;
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
            IZombieComponent baseZombie = new RegularZombie();
            return type switch
            {
                "Regular" => baseZombie,
                "Cone" => new ConeDecorator(baseZombie, this), // Pass 'this' factory for potential transformation
                "Bucket" => new BucketDecorator(baseZombie, this), // Pass 'this' factory
                "Screendoor" => new ScreendoorDecorator(baseZombie, this), // Pass 'this' factory
                _ => throw new ArgumentException("Invalid zombie type", nameof(type)),
            };
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
