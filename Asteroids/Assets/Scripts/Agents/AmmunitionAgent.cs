using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class AmmunitionAgent
    {
        public Dictionary<string, GameObject> ammunitionPrefabs = new Dictionary<string, GameObject>();
        public Dictionary<string, Ammunition> ammunitionBehaviors = new Dictionary<string, Ammunition>();

        public List<string> ammunitionNames = new List<string>();

        private int index = 0;

        public AmmunitionAgent(GameObject ammunitionPrefab)
        {
            AddAmmunition(ammunitionPrefab);
        }
        public AmmunitionAgent(GameObject[] ammunitionPrefabs)
        {
            for (int i = 0; i < ammunitionPrefabs.Length; i++) AddAmmunition(ammunitionPrefabs[i]);
        }

        public int Current => index;
        public string CurrentAmmunitionName => ammunitionNames[index];
        public Ammunition CurrentAmmunitionBehavior => ammunitionBehaviors[ammunitionNames[index]];
        public GameObject CurrentAmmunitionPrefab => ammunitionPrefabs[ammunitionNames[index]];

        public void NextAmmunition()
        {
            if (index == ammunitionNames.Count - 1) index = 0;
            else index++;
        }

        public void SelectAmmunition(string ammunitionName)
        {
            if (ammunitionNames.Contains(ammunitionName)) index = ammunitionNames.IndexOf(ammunitionName);
            else throw new System.Exception("Invalid ammunition name");
        }

        public void SelectAmmunition(int ammunitionIndex)
        {
            index = ammunitionIndex;
        }

        public void AddAmmunition(GameObject ammunitionPrefab)
        {
            if (!ammunitionNames.Contains(ammunitionPrefab.name))
            {
                ammunitionNames.Add(ammunitionPrefab.name);
                ammunitionPrefabs.Add(ammunitionPrefab.name, ammunitionPrefab);
                if (ammunitionPrefab.TryGetComponent(out Ammunition ammunitionBehavior)) ammunitionBehaviors.Add(ammunitionPrefab.name, ammunitionBehavior);
                else throw new System.Exception("No behavior script found");
            }
            else throw new System.Exception("This type of ammo already exists");
        }
    }
}