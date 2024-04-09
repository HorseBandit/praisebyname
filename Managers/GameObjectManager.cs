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

        public void HandleZombieTransformation(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            // Replace the oldZombie with newZombie in the list
            int index = zombies.IndexOf(oldZombie);
            if (index != -1)
            {
                zombies[index] = newZombie;
                // Make sure to unsubscribe from the oldZombie's events to avoid memory leaks
                //UnsubscribeFromZombieEvents(oldZombie);
                Console.WriteLine($"A {oldZombie.Type} Zombie transformed into a {newZombie.Type}!");
            }
        }

        /*private void UnsubscribeFromZombieEvents(IZombieComponent zombie)
        {
            if (zombie is ConeZombie coneZombie)
            {
                coneZombie.OnTransformation -= HandleZombieTransformation;
            }
            // Repeat for other zombie types as needed
        }*/

        // You might need a method in GameObjectManager to replace a zombie object.
        public void ReplaceZombie(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            int index = zombies.IndexOf(oldZombie);
            if (index != -1)
            {
                zombies[index] = newZombie; // Replace the old zombie with the new one.
            }
        }

        // Additional functionality to manage plants and bullets can be added here
    }
}
