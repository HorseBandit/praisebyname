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
    public class GameEventManager : IGameEntityObserver
    {
        private GameObjectManager gameObjectManager;

        public GameEventManager(GameObjectManager manager)
        {
            this.gameObjectManager = manager;
            // Subscribe to the GameObjectManager's notifications
            gameObjectManager.Subscribe(this);
        }

        // Implementation of the IGameEntityObserver interface
        public void Notify(IZombieComponent zombie, string eventType)
        {
            // Handle different types of events
            switch (eventType)
            {
                case "ZombieAdded":
                    Console.WriteLine($"Zombie added: {zombie.Type}");
                    break;
                case "ZombieRemoved":
                    Console.WriteLine($"Zombie removed: {zombie.Type}");
                    // Handle additional logic for zombie removal, if necessary
                    break;
                case "ZombieTransformed":
                    Console.WriteLine($"Zombie transformed: {zombie.Type}");
                    // Handle transformation logic, if necessary
                    break;
                case "ZombieReplaced":
                    Console.WriteLine($"Zombie replaced with new type: {zombie.Type}");
                    // Handle replacement logic, if necessary
                    break;
                default:
                    Console.WriteLine("Unhandled event type.");
                    break;
            }
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
                        if (zombie.HasMetal) // Ensure this check or logic is implemented in the TakeDamage method
                        {
                            zombie.TakeDamage(0, StrikeType.MushroomExtract);
                        }
                        break;
                }
            }
        }

        // Ensure to unsubscribe when the GameEventManager is no longer in use
        public void Cleanup()
        {
            gameObjectManager.Unsubscribe(this);
        }
    }
    /*class GameEventManager
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
    }*/
}
