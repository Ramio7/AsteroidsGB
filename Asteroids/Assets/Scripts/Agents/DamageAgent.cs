using System;
using UnityEngine;

namespace RRRStudyProject
{
    public class DamageAgent : ITakeDamage, IHeal
    {
        private readonly GameObject _agentInitializer;
        private int _pointsForObjectDestroy;

        public float maxHealth;
        public float currentHealth;

        public event Action<int> ObjectDestroyed = delegate (int points) { };
        public event Action<string> DestroyedObjectInfo = delegate (string name) { };

        public DamageAgent(GameObject agentInitializer)
        {
            _agentInitializer = agentInitializer;

            if (_agentInitializer.TryGetComponent(out IDamageInitializer agentClass))
            {
                maxHealth = agentClass.Data.MaxHealth;
                currentHealth = agentClass.Data.Health;
            }
            else throw new Exception("No monobehavior script of damage initializer found");

            _pointsForObjectDestroy = (int)maxHealth * 10;
            
            if (agentClass.GetType().Name != "Player")
            {
                ScoreBar scoreBar = (ScoreBar)UnityEngine.Object.FindObjectOfType<MainGame>().interactiveObjects.GetExecute("ScoreBar");
                ObjectDestroyed += scoreBar.AddScore;
                MessageBar messageBar = (MessageBar)UnityEngine.Object.FindObjectOfType<MainGame>().interactiveObjects.GetExecute("MessageBar");
                DestroyedObjectInfo += messageBar.ObjectDestroyed;
            }
        }

        public void Hit(float incomingDamage)
        {
            currentHealth -= incomingDamage;
            if (currentHealth <= 0)
            {
                _agentInitializer.SetActive(false);
                ObjectDestroyed.Invoke(_pointsForObjectDestroy);
                DestroyedObjectInfo.Invoke(_agentInitializer.name);
            }
        }

        public void Heal(float healDamage)
        {
            currentHealth += healDamage;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
}