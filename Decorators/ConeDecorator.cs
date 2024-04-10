using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
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
        public override string Type => "Cone";

        public string Id { get; private set; }
        public ConeDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager, int health)
        : base(zombie, zombieFactory, gameObjectManager) 
        {
            this.HasAccessory = true;
            this.HasMetal = false;
            this._zombie.SetHealth(health);
            this._gameObjectManager = gameObjectManager;
            Id = Guid.NewGuid().ToString();
        }

        public override (bool success, DmgOutcome outcome) TakeDamage(int damage, StrikeType strikeType)
        {
            if(HasAccessory)
            {
                KnockAccessory();
                // Apply damage directly, bypassing accessory checks
                return (false, DmgOutcome.AccessoryRemoved);
            }
            else if (strikeType == StrikeType.WatermelonOverhead || strikeType == StrikeType.Normal)
            {
                return (true, DmgOutcome.DamageDealt); // Indicates the accessory absorbed the damage
            }
            else
            {
                // If there's no accessory or it's already lost, delegate damage to the wrapped component
                return (true, DmgOutcome.DamageDealt);
            }
        }
    }
}
