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
using MidtermOneSWE.Managers;

namespace MidtermOneSWE.Decorators
{
    class BucketDecorator : ZombieDecorator
    {
        public override string Type => "Bucket";
        public BucketDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager, int health)
        : base(zombie, zombieFactory, gameObjectManager)
        {
            this.HasAccessory = true;
            this.HasMetal = true;
            this._zombie.SetHealth(health);
            this._gameObjectManager = gameObjectManager;
        }

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            if (strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory();
                return false;
            }
            else if (HasAccessory)
            {
                KnockAccessory();
                return true; // Indicates the accessory absorbed the damage
            }
            else
            {
                // If there's no accessory or it's already lost, delegate damage to the wrapped component
                return base._zombie.TakeDamage(damage, strikeType);
            }
        }
    }
}

