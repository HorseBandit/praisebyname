using MidtermOneSWE.Interfaces;
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

        public void RequestZombieTransformation(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            gameObjectManager.HandleZombieTransformation(oldZombie, newZombie);
        }

        public void SimulateCollisionDetection(int plantType)
        {
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
                        zombie.TakeDamage(0, StrikeType.MushroomExtract);
                        break;
                }
            }
        }
    }
}
