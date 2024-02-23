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
        private Random random = new Random();
        public IZombieComponent CreateZombie(string type)
        {
            return type switch
            {
                "Regular" => new RegularZombie(),
                "Cone" => new ConeZombie(this),
                "Bucket" => new BucketZombie(),
                "Screendoor" => new ScreendoorZombie(),
                "Group" => CreateZombieGroup(),
                _ => throw new ArgumentException("Invalid zombie type", nameof(type)),
            };
        }

        private IZombieComponent CreateZombieGroup()
        {
            ZombieGroup group = new ZombieGroup();
            int groupSize = random.Next(2, 6); // Random group size between 2 and 5

            for (int i = 0; i < groupSize; i++)
            {
                group.Add(CreateRandomZombie());
            }

            return group;
        }

        private IZombieComponent CreateRandomZombie()
        {
            string[] types = new string[] { "Regular", "Cone", "Bucket", "Screendoor" };
            int index = random.Next(types.Length);
            return CreateZombie(types[index]);
        }
    }

}
