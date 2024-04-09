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
        public ConeDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager, int health)
        : base(zombie, zombieFactory, gameObjectManager) // Ensure base class also accepts and handles GameObjectManager
        {
            this.HasAccessory = true;
            this.HasMetal = false;
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


        /*protected void TransformToRegular()
        {
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");

            // Assuming '_gameObjectManager' is a field set to the instance of GameObjectManager available to this decorator.
            _gameObjectManager.ReplaceZombie(this, regularZombie);

            Console.WriteLine($"{this.Type} has lost its accessory and is now a Regular Zombie.");
        }*/

        /*protected override void KnockAccessory()
        {
            HasAccessory = false;
            // Print a message about losing the accessory, if not handled elsewhere
            TransformToRegular(); // Handles transforming into a regular zombie
        }*/
    }
}
