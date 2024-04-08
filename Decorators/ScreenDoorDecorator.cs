using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Decorators
{
    class ScreenDoorDecorator : ZombieDecorator
    {
        public ScreenDoorDecorator(IZombieComponent zombie) : base(zombie)
        {
            HasAccessory = true;
            HasMetal = false; // Assuming cones are not metal
        }

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            // Specific logic for BucketDecorator
            if (HasAccessory && (strikeType == StrikeType.Normal || strikeType == StrikeType.WatermelonOverhead))
            {
                KnockAccessory();
                return false;
            }
            return _zombie.TakeDamage(damage, strikeType);
        }

        private void KnockAccessory()
        {
            HasAccessory = false;
            Console.WriteLine($"{Type} Zombie's cone knocked off!");
        }
    }
}
