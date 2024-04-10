using MidtermOneSWE.Interfaces;
using MidtermTwoSWE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Managers
{

    public class GameObjectManager
    {
        private List<IZombieComponent> zombies = new List<IZombieComponent>();
        private List<IGameEntityObserver> observers = new List<IGameEntityObserver>(); // Observers list

        // Allows observers to subscribe for notifications
        public void Subscribe(IGameEntityObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        // Allows observers to unsubscribe from notifications
        public void Unsubscribe(IGameEntityObserver observer)
        {
            observers.Remove(observer);
        }

        // Notify all observers about an event
        private void NotifyObservers(IZombieComponent zombie, string eventType)
        {
            foreach (var observer in observers)
            {
                observer.Notify(zombie, eventType);
            }
        }

        // Adds a zombie and notifies observers about the addition
        public void AddZombie(IZombieComponent zombie)
        {
            zombies.Add(zombie);
            NotifyObservers(zombie, "ZombieAdded"); // Notify observers of a new zombie
        }

        // Removes a zombie and notifies observers about the removal
        public void RemoveZombie(IZombieComponent zombie)
        {
            if (zombies.Remove(zombie))
            {
                NotifyObservers(zombie, "ZombieRemoved"); // Notify observers of zombie removal
            }
        }

        public IEnumerable<IZombieComponent> GetAllZombies()
        {
            return zombies;
        }

        // Handles the transformation of a zombie and notifies observers
        public void HandleZombieTransformation(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            // Replace the oldZombie with newZombie in the list
            int index = zombies.IndexOf(oldZombie);
            if (index != -1)
            {
                zombies[index] = newZombie;
                NotifyObservers(newZombie, "ZombieTransformed"); // Notify observers of zombie transformation
                Console.WriteLine($"A {oldZombie.Type} Zombie transformed into a {newZombie.Type}!");
            }
        }

        public void ReplaceZombie(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            int index = zombies.IndexOf(oldZombie);
            if (index != -1)
            {
                zombies[index] = newZombie; // Replace the old zombie with the new one.
                NotifyObservers(newZombie, "ZombieReplaced"); // Optionally notify observers of zombie replacement
            }
        }
    }


    /*class GameObjectManager
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
                Console.WriteLine($"A {oldZombie.Type} Zombie transformed into a {newZombie.Type}!");
            }
        }

        public void ReplaceZombie(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            int index = zombies.IndexOf(oldZombie);
            if (index != -1)
            {
                zombies[index] = newZombie; // Replace the old zombie with the new one.
            }
        }

    }*/
}
