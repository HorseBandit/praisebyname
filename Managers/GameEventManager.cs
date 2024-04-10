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

        // This method is an example of actively checking game conditions or states
        // and responding by directly manipulating the game state.
        public void UpdateGameState()
        {
            // Example implementation - adjust according to your game's logic and requirements

            // Check game conditions or listen to specific game triggers
            // and act upon them directly through GameObjectManager or other game components.

            // This method should be called within your game loop or event processing loop.
        }

        // This method directly requests the transformation of a zombie to another type.
        public void RequestZombieTransformation(IZombieComponent oldZombie, string newZombieType)
        {
            // Assuming oldZombie has an 'Id' property.
            // First, ensure that the 'Id' property is accessible within IZombieComponent.
            var newZombie = gameObjectManager.TransformZombie(oldZombie.Id, newZombieType);
            if (newZombie != null)
            {
                Console.WriteLine($"Transformed a zombie into a {newZombieType}.");
            }
        }

        // Example method that could simulate collision detection and apply effects based on plant types.
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
                        if (zombie.HasMetal) // Assuming a HasMetal property or similar mechanism
                        {
                            Console.WriteLine("Magnet-shroom effect applied: Accessory removed from a metal zombie.");
                            // Specific logic to remove metal accessory or transform the zombie
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid plant type.");
                        break;
                }
            }
        }

        // You might include other methods that actively manage different aspects of the game,
        // such as responding to user inputs, managing game levels, or updating scores.
    }
}