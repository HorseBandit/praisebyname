using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Factories
{
    class ZombieFactory : IZombieFactory
    {
        public IZombieComponent CreateZombie(string type)
        {
            return type switch
            {
                "Regular" => new RegularZombie(),
                "Cone" => new ConeZombie(),
                "Bucket" => new BucketZombie(),
                "Screendoor" => new ScreendoorZombie(),
                _ => throw new ArgumentException("Invalid zombie type", nameof(type)),
            };
        }
    }

}
