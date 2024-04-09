using MidtermOneSWE.ConcreteZombies;
using MidtermOneSWE.Interfaces;
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

        public virtual string Type => _zombie.Type;
        public int Health => _zombie.Health;
        public bool HasAccessory { get; protected set; }
        public bool HasMetal { get; protected set; }

        public event ZombieTransformationHandler OnTransformation;

        public ZombieDecorator(IZombieComponent zombie, IZombieFactory zombieFactory)
        {
            _zombie = zombie;
            _zombieFactory = zombieFactory;
            HasAccessory = true; // Assuming all decorators initially have an accessory
        }

        public void SetHealth(int newHealth)
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

        protected virtual void KnockAccessory()
        {
            HasAccessory = false;
            Console.WriteLine($"{Type} Zombie's accessory knocked off!");

            // This method should handle the transformation to a regular zombie if needed.
            // Since this is a base class method, specific decorators may choose to override this behavior.
            if (this.GetType() != typeof(RegularZombie)) // Ensure not already a RegularZombie
            {
                // Assuming RegularZombie transformation logic here; adjust as necessary.
                IZombieComponent regularZombie = _zombieFactory.CreateZombie("Regular");
                regularZombie.SetHealth(this.Health);

                // Invoke the transformation event if subscribers exist
                OnTransformation?.Invoke(this, regularZombie);
            }
        }
    }
}

