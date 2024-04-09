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

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            if (strikeType == StrikeType.MushroomExtract && HasMetal)
            {
                KnockAccessory(); // Remove the screendoor accessory
                return false; // Indicate no damage was dealt
            }
            else if (strikeType == StrikeType.WatermelonOverhead)
            {
                // Apply damage directly, bypassing accessory checks
                return base._zombie.TakeDamage(damage, strikeType);
            }
            else if (HasAccessory)
            {
                // For other attacks, behave normally according to the decorator's logic
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
