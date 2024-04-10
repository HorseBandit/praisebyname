using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermTwoSWE.Interfaces
{
    public interface IGameEntityObserver
    {
        void Notify(IZombieComponent zombie, string eventType);
    }
}
