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
    class ScreenDoorDecorator : ZombieDecorator
    {
        public override string Type => "ScreenDoor";
        public ScreenDoorDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager, int health)
        : base(zombie, zombieFactory, gameObjectManager)
        {
            this.HasAccessory = true;
            this.HasMetal = true;
            this._zombie.SetHealth(health);
            this._gameObjectManager = gameObjectManager; 
        }

        public override (bool success, DmgOutcome outcome) TakeDamage(int damage, StrikeType strikeType)
        {
            if (strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory(); // Remove the screendoor accessory
                return (false, DmgOutcome.AccessoryRemoved); // Indicate no damage was dealt
            }
            else if (strikeType == StrikeType.WatermelonOverhead)
            {
                var result = base._zombie.TakeDamage(damage, strikeType);
                return (result.success, result.outcome == DmgOutcome.DamageDealt ? DmgOutcome.DamageDealt : DmgOutcome.NoEffect);
            }
            else if (HasAccessory)
            {
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
