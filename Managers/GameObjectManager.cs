﻿using MidtermOneSWE.Factories;
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
        private ZombieFactory zombieFactory; // Hold a reference to the factory, but it's not set in the constructor

        // Constructor does not require the factory parameter
        public GameObjectManager()
        {
        }

        // Method to inject the ZombieFactory instance after instantiation
        public void SetZombieFactory(ZombieFactory factory)
        {
            this.zombieFactory = factory;
        }

        public void AddZombie(IZombieComponent zombie)
        {
            zombies.Add(zombie);
        }

        public bool RemoveZombie(IZombieComponent zombie)
        {
            return zombies.Remove(zombie);
        }

        public IEnumerable<IZombieComponent> GetAllZombies()
        {
            return zombies.AsReadOnly();
        }

        // Now, assuming you need to transform a zombie inside GameObjectManager using the factory
        public IZombieComponent TransformZombie(string oldZombieId, string newZombieType)
        {
            var oldZombie = zombies.FirstOrDefault(z => z.Id == oldZombieId);
            if (oldZombie != null)
            {
                var newZombie = zombieFactory.CreateZombie(newZombieType);
                if (newZombie != null)
                {
                    RemoveZombie(oldZombie);
                    AddZombie(newZombie);
                    return newZombie;
                }
            }
            return null;
        }

        public void ReplaceZombie(IZombieComponent oldZombie, IZombieComponent newZombie)
        {
            int index = zombies.FindIndex(z => z.Id == oldZombie.Id);
            if (index != -1)
            {
                // Replace the old zombie with the new zombie in the list
                zombies[index] = newZombie;
            }
        }

        // Additional methods as necessary...
    }
}
