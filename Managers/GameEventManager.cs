using MidtermOneSWE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Managers
{
    class GameEventManager
    {
        private GameObjectManager gameObjectManager;

        public GameEventManager(GameObjectManager manager)
        {
            this.gameObjectManager = manager;
        }

        public void SimulateCollisionDetection(int plantType)
        {
            // Example of how to access and interact with zombies
            foreach (var zombie in gameObjectManager.GetAllZombies())
            {
                switch (plantType)
                {
                    case 1: // Peashooter attack
                        zombie.TakeDamage(25, StrikeType.Normal);
                        break;
                    case 2: // Watermelon attack
                        zombie.TakeDamage(30, StrikeType.WatermelonOverhead);
                        break;
                    case 3: // Magnet-shroom attack
                        // Assuming a way to determine if a zombie has a metal accessory
                        if (zombie.HasMetal)
                        {
                            // Implement logic to remove metal accessory
                            // This might involve transforming the zombie
                        }
                        break;
                }
            }
        }

        // Additional methods to simulate other types of collisions or interactions
    }
}
