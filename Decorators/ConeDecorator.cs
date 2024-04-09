using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Decorators
{
    class ConeDecorator : ZombieDecorator
    {
        /*public ConeDecorator(IZombieComponent zombie, IZombieFactory zombieFactory)
            : base(zombie, zombieFactory) // Ensure the factory is passed to the base class
        {
            HasAccessory = true;
            HasMetal = false; // Assuming cones are not metal
        }*/
        public override string Type => "Cone";

        public ConeDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, int health) : base(zombie, zombieFactory)
        {
            this.HasAccessory = true; // Assuming cones are not metal.
            this.HasMetal = false;
            this._zombie.SetHealth(health); // Directly set the health of the decorated zombie.
        }

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            // Specific logic for ConeDecorator
            if (HasAccessory && (strikeType == StrikeType.Normal || strikeType == StrikeType.WatermelonOverhead))
            {
                KnockAccessory();
                return false; // Indicates the accessory was knocked off but no damage was taken
            }
            return base.TakeDamage(damage, strikeType); // Delegate to the wrapped zombie object if no accessory logic is triggered
        }

        protected override void KnockAccessory()
        {
            base.KnockAccessory(); // Call to the base method for any common behavior
            HasAccessory = false;
            Console.WriteLine($"{_zombie.Type} Zombie's cone knocked off!");

            // Transform the current zombie into a RegularZombie.
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);

            // Notify about the transformation
            RaiseOnTransformation(this, regularZombie);
        }
    }
}
