using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Interfaces
{
    interface IZombieFactory
    {
        IZombieComponent CreateZombie(string type);
    }

}
