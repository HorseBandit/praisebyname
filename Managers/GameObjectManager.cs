using MidtermOneSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Managers
{
    class GameObjectManager
    {
        private List<IZombieComponent> zombies = new List<IZombieComponent>();

        public void AddZombie(IZombieComponent zombie)
        {
            zombies.Add(zombie);
        }

        public void RemoveZombie(IZombieComponent zombie)
        {
            zombies.Remove(zombie);
        }

        public IEnumerable<IZombieComponent> GetAllZombies()
        {
            return zombies;
        }

        // Additional functionality to manage plants and bullets can be added here
    }
}
