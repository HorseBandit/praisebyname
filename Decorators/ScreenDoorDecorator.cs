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
        : base(zombie, zombieFactory, gameObjectManager) // Ensure base class also accepts and handles GameObjectManager
        {
            this.HasAccessory = true;
            this.HasMetal = true;
            this._zombie.SetHealth(health);
            this._gameObjectManager = gameObjectManager; // Assuming _gameObjectManager is declared in ZombieDecorator
        }

        public override bool TakeDamage(int damage, StrikeType strikeType)
        {
            if (HasAccessory)
            {
                KnockAccessory(); // Calls the base implementation which now handles transformation
                return true; // Indicates the accessory absorbed the damage
            }
            else
            {
                // Delegate damage to the wrapped component if the accessory is already lost
                return _zombie.TakeDamage(damage, strikeType);
            }
        }
    }
}
