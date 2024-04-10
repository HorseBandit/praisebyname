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

        public string Id { get; private set; }
        public BucketDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager, int health)
        : base(zombie, zombieFactory, gameObjectManager)
        {
            this.HasAccessory = true;
            this.HasMetal = true;
            this._zombie.SetHealth(health);
            this._gameObjectManager = gameObjectManager;
            Id = Guid.NewGuid().ToString();
        }

        public override (bool success, DmgOutcome outcome) TakeDamage(int damage, StrikeType strikeType)
        {
            if (strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory(); // Remove the screendoor accessory
                return (false, DmgOutcome.AccessoryRemoved); // Indicate no damage was dealt
            }
            else if (HasAccessory)
            {
                // For other attacks, behave normally according to the decorator's logic
                KnockAccessory();
                return (false, DmgOutcome.AccessoryRemoved); // Indicates the accessory absorbed the damage
            }
            else
            {
                // If there's no accessory or it's already lost, delegate damage to the wrapped component
                return (true, DmgOutcome.DamageDealt);
            }
        }
    }
}

