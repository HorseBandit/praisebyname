using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Decorators
{
    abstract class ZombieDecorator : IZombieComponent
    {
        protected IZombieComponent _zombie;

        public string Type => _zombie.Type;
        public int Health => _zombie.Health;
        public bool HasAccessory { get; protected set; }
        public bool HasMetal { get; protected set; }

        public ZombieDecorator(IZombieComponent zombie)
        {
            _zombie = zombie;
        }

        public abstract bool TakeDamage(int damage, StrikeType strikeType);

        public void Die()
        {
            _zombie.Die();
        }
    }
}

