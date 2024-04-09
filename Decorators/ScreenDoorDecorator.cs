using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Decorators
{
    class ScreendoorDecorator : ZombieDecorator
    {
        public override string Type => "ScreenDoor";
        public ScreendoorDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, int health)
         : base(zombie, zombieFactory)
        {
            this.HasAccessory = true;
            this.HasMetal = true; // Assuming screendoors are considered metal
            this._zombie.SetHealth(health); // Adjust the decorated zombie's health
        }

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            if (HasAccessory && strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory();
                return false;
            }
            else if (HasAccessory && (strikeType == StrikeType.Normal || strikeType == StrikeType.WatermelonOverhead))
            {
                KnockAccessory();
                return false;
            }

            return base.TakeDamage(damage, strikeType);
        }

        protected override void KnockAccessory()
        {
            base.KnockAccessory(); // Call to the base method if needed for common behavior
            HasAccessory = false;
            HasMetal = false;
            Console.WriteLine($"{_zombie.Type} Zombie's screendoor knocked off!");

            // Transform the current zombie into a RegularZombie.
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);

            // Notify about the transformation
            RaiseOnTransformation(this, regularZombie);
        }
    }
}
