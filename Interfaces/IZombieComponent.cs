using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Interfaces
{
    interface IZombieComponent
    {
        string Type { get; }
        int Health { get; }
        void TakeDamage(int damage);
        void Die();
    }

}
