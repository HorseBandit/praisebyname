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

        public ZombieDecorator(IZombieComponent zombie, IZombieFactory zombieFactory, GameObjectManager gameObjectManager)
        {
            _zombie = zombie;
            _zombieFactory = zombieFactory;
            _gameObjectManager = gameObjectManager;
            HasAccessory = true;
        }

        public virtual void SetHealth(int newHealth)
        {
            _zombie.SetHealth(newHealth);
        }

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

        // Accessory loss and potential transformation
        protected virtual void KnockAccessory()
        {
            if (HasAccessory)
            {
                HasAccessory = false;
                TransformToRegular();
            }
        }

        protected void TransformToRegular()
        {
            IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
            regularZombie.SetHealth(this.Health);
            _gameObjectManager.ReplaceZombie(this, regularZombie);
            OnTransformation?.Invoke(this, regularZombie);
            Console.WriteLine($"{this.Type} has lost its accessory and is now a Regular Zombie.");
        }
    }
}