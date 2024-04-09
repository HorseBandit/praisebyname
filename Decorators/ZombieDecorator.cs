using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Managers;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MidtermOneSWE.Interfaces.IZombieComponent;

namespace MidtermOneSWE.Decorators
{
    abstract class ZombieDecorator : IZombieComponent
    {
        protected IZombieComponent _zombie;
        protected IZombieFactory _zombieFactory;
        protected GameObjectManager _gameObjectManager;

        public virtual string Type => _zombie.Type;
        public int Health => _zombie.Health;
        public bool HasAccessory { get; protected set; }
        public bool HasMetal { get; protected set; }

        public event ZombieTransformationHandler OnTransformation;

        // Adjust the constructor to accept GameObjectManager
        public ZombieDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager)
        {
            _zombie = zombie;
            _zombieFactory = zombieFactory;
            _gameObjectManager = gameObjectManager; // Set the GameObjectManager reference
            HasAccessory = true; // Assuming all decorators initially have an accessory
        }

        public virtual void SetHealth(int newHealth)
        {
            _zombie.SetHealth(newHealth);
        }

        // Ensure this method is accessible to derived classes if they need to raise the event
        protected void RaiseOnTransformation(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            OnTransformation?.Invoke(oldZombie, newZombie);
        }

        public virtual bool TakeDamage(int damage, StrikeType strikeType)
        {
            // Delegate damage handling to the wrapped zombie object
            return _zombie.TakeDamage(damage, strikeType);
        }

        public void Die()
        {
            _zombie.Die();
        }

        // Updated to correctly handle accessory loss and potential transformation
        protected virtual void KnockAccessory()
        {
            if (HasAccessory)
            {
                HasAccessory = false;
                TransformToRegular(); // Handle transformation here
            }
        }

        // Now centralized in the base class for all types of zombies with accessories
        protected void TransformToRegular()
        {
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);

            _gameObjectManager.ReplaceZombie(this, regularZombie);

            // Optionally, notify about the transformation if there are subscribers
            OnTransformation?.Invoke(this, regularZombie);
            Console.WriteLine($"{this.Type} has lost its accessory and is now a Regular Zombie.");
        }
    }
}

