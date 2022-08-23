using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RRRStudyProject
{
    public class DamageAgent : ITakeDamage, IHeal
    {
        private readonly GameObject _agentInitializer;
        public float maxHealth;
        public float currentHealth;

        event Action<MessagePayload<DamageAgent>> ObjectDestroyed = delegate (MessagePayload<DamageAgent> message) { };

        public DamageAgent(GameObject agentInitializer)
        {
            _agentInitializer = agentInitializer;

            if (_agentInitializer.TryGetComponent(out IDamageInitializer agentClass))
            {
                maxHealth = agentClass.Data.MaxHealth;
                currentHealth = agentClass.Data.Health;
            }
            else throw new Exception("No monobehavior script of damage initializer found");
        }

        public void Hit(float incomingDamage)
        {
            currentHealth -= incomingDamage;
            if (currentHealth <= 0)
            {
                _agentInitializer.SetActive(false);
                ObjectDestroyed.Invoke(new MessagePayload<DamageAgent>(this, _agentInitializer));
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