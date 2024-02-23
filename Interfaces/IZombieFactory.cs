using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Interfaces
{
    /// <summary>
    /// Interface for zombie factories.
    /// </summary>
    interface IZombieFactory
    {
        IZombieComponent CreateZombie(string type);
    }
}