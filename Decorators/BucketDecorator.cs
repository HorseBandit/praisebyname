﻿using System;
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

