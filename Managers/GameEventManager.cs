using MidtermOneSWE.Interfaces;
using MidtermOneSWE.Utilities;
using MidtermTwoSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Managers
{
    public class GameEventManager
    {
        private GameObjectManager gameObjectManager;

        public GameEventManager(GameObjectManager manager)
        {
            this.gameObjectManager = manager;
        }

        public void UpdateGameState()
        {
            
        }

        // This method directly requests the transformation of a zombie to another type.
        public void RequestZombieTransformation(IZombieComponent oldZombie, string newZombieType)
        {
            var newZombie = gameObjectManager.TransformZombie(oldZombie.Id, newZombieType);
            if (newZombie != null)
            {
                Console.WriteLine($"Transformed a zombie into a {newZombieType}.");
            }
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
                        if (zombie.HasMetal)
                        {
                            Console.WriteLine("Magnet-shroom effect applied: Accessory removed from a metal zombie.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid plant type.");
                        break;
                }
            }
        }
    }
}