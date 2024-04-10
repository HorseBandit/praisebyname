using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Interfaces
{
    public delegate void ZombieTransformationHandler(IZombieComponent oldZombie, IZombieComponent newZombie);

    public interface IZombieComponent
    {
        event ZombieTransformationHandler OnTransformation;

        string Id { get; }
        string Type { get; }
        int Health { get; }
        bool HasAccessory { get; }
        bool HasMetal { get; }

        (bool success, DmgOutcome outcome) TakeDamage(int damage, StrikeType strikeType);

        void Die();
        void SetHealth(int newHealth);
    }
}
