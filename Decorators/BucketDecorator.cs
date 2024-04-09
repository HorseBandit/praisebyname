using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidtermOneSWE.Factories;
using MidtermOneSWE.ConcreteZombies;

namespace MidtermOneSWE.Decorators
{
    class BucketDecorator : ZombieDecorator
    {
        private IZombieFactory _zombieFactory;

        public BucketDecorator(IZombieComponent zombie, IZombieFactory zombieFactory)
            : base(zombie, zombieFactory) // Ensure the factory is passed to the base class
        {
            HasAccessory = true;
            HasMetal = false; // Assuming cones are not metal
        }

        /*public BucketDecorator(IZombieComponent zombie, IZombieFactory zombieFactory) : base(zombie)
        {
            HasAccessory = true;
            HasMetal = true;
            _zombieFactory = zombieFactory;
        }*/
        /*public BucketDecorator(IZombieComponent zombie) : base(zombie)
        {
            // Assuming the bucket is a metal accessory
            HasAccessory = true;
            HasMetal = true;
        }*/

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            // Specific logic for BucketDecorator
            if (HasAccessory && strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory();
                // If it's a MushroomExtract attack and the zombie has a metal accessory (bucket),
                // the accessory is removed, but no damage is taken.
                return false;
            }
            else if (HasAccessory && (strikeType == StrikeType.Normal || strikeType == StrikeType.WatermelonOverhead))
            {
                // If the zombie is hit by a normal or WatermelonOverhead strike, the bucket is removed,
                // but let's assume in this case it does protect the zombie one time, so no damage is taken.
                KnockAccessory();
                return false;
            }

            // If the accessory is already knocked off or the attack doesn't target the accessory specifically,
            // delegate the damage to the wrapped zombie object.
            return _zombie.TakeDamage(damage, strikeType);
        }

        protected override void KnockAccessory()
        {
            base.KnockAccessory(); // Adjust HasAccessory and HasMetal as needed.
            Console.WriteLine($"{_zombie.Type} Zombie's bucket knocked off!");

            // Transform the current zombie into a RegularZombie.
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
            if (regularZombie is RegularZombie rz)
            {
                rz.SetHealth(this.Health);

                // Use the protected method to raise the event
                RaiseOnTransformation(this, regularZombie);
            }
        }

        /*private void KnockAccessory()
        {
            HasAccessory = false;
            HasMetal = false; // Once the bucket is knocked off, it's no longer metal-affected.
            Console.WriteLine($"{_zombie.Type} Zombie's bucket knocked off!");
        }*/
    }
}

