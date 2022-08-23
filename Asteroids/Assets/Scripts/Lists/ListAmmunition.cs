using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class ListAmmunition
    {
        public Dictionary<string, Ammunition> ammunitionBehaviors = new Dictionary<string, Ammunition>();
        public Dictionary<string, int> ammunitionAmount = new Dictionary<string, int>();

        public List<string> ammunitionNames = new List<string>();

        private int index = 0;
        public int Current => index;
        public string CurrentAmmunitionName => ammunitionNames[index];
        public Ammunition CurrentAmmunitionBehavior => ammunitionBehaviors[ammunitionNames[index]];
        public int CurrentAmmunitionAmount { get => ammunitionAmount[ammunitionNames[index]]; set => ammunitionAmount[ammunitionNames[index]] = value; } 

        private readonly AmmunitionFactory _ammunitionFactory = Object.FindObjectOfType<MainGame>().gameFactories.ammunitionFactory;
        public ListAmmunition()
        {
            AddAmmunition(_ammunitionFactory.StandartBulletBehavior());
        }
        public void NextAmmunition()
        {
            if (index == ammunitionNames.Count - 1) index = 0;
            else index++;
        }

        public void SelectAmmunition(string ammunitionName)
        {
            if (ammunitionNames.Contains(ammunitionName)) index = ammunitionNames.IndexOf(ammunitionName);
            else throw new Exception("Invalid ammunition name");
        }

        public void SelectAmmunition(int ammunitionIndex)
        {
            index = ammunitionIndex;
        }

        public void AddAmmunition(Ammunition ammunitionBehavior)
        {
            if (!ammunitionNames.Contains(ammunitionBehavior.ammoType))
            {
                ammunitionNames.Add(ammunitionBehavior.ammoType);
                ammunitionBehaviors.Add(ammunitionBehavior.ammoType, ammunitionBehavior);
                ammunitionAmount.Add(ammunitionBehavior.ammoType, ammunitionBehavior.ammoAmountBonus);
                return;
            }
            else 
            {
                ammunitionAmount[ammunitionBehavior.ammoType] += ammunitionBehavior.ammoAmountBonus;
                return;
            }
            throw new Exception("Failed to add new ammo");
        }
    }
}