using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Interfaces
{
    /// <summary>
    /// Interface for ZombieComponents. This interface describes each zombie's required attributes and methods.
    /// </summary>
    interface IZombieComponent
    {

        public delegate void ZombieTransformationHandler(IZombieComponent oldZombie, IZombieComponent newZombie);
        string Type { get; }
        int Health { get; }

        bool TakeDamage(int damage);
        //void TakeDamage(int damage);
        void Die();
    }
}
