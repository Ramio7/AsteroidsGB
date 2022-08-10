using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class DamageAgent : ITakeDamage, IDoDamage, IHeal, IHaveCooldowns //закончить рефакторинг
    {
        private readonly GameObject _agentInitializer;
        public float maxHealth;
        public float currentHealth;

        private readonly AmmunitionAgent _ammunitionAgent;
        private float _ammoCooldownStart;
        private readonly Transform _ammoStartTransform;

        private readonly CommandInput _commandInput;

        private IViewServices _viewServices;


        public DamageAgent(GameObject agentInitializer)
        {
            _agentInitializer = agentInitializer;
            
            _viewServices = Object.FindObjectOfType<MainGame>().viewServices;
            if (_viewServices == null) throw new Exception("No ViewServices found on scene");

            if (_agentInitializer.TryGetComponent(out ITakeCommands commandInput))
            {
                _commandInput = commandInput.CommandInput;
            }
            else throw new Exception("No command input found. Add one, please");

            if (_agentInitializer.TryGetComponent(out IDamageInitializer agentClass))
            {
                maxHealth = agentClass.Data.MaxHealth;
                currentHealth = agentClass.Data.Health;
            }
            else throw new Exception("No monobehavior script of damage initializer found");

            if (_agentInitializer.TryGetComponent(out ICarryWeapons objectAmmunition))
            {
                _ammunitionAgent = objectAmmunition.AmmunitionAgent;
                Debug.Log(_ammunitionAgent.Current);
                if (_ammunitionAgent == null) throw new Exception("No ammunition agent found. Add ammunition agent to object");

                _ammoCooldownStart = Time.time;

                _ammoStartTransform = _agentInitializer.transform.Find("AmmoStartPosition");
                if (_ammoStartTransform == null) throw new Exception("Add empty 'AmmoStartPosition' to object or object prefab");
            }
        }

        public void Fire()
        {
            if (!_commandInput.weaponLock)
            {
                if (!Cooldown(_ammoCooldownStart, _ammunitionAgent.CurrentAmmunitionBehavior.ammoCooldown, out _ammoCooldownStart)) _commandInput.isFiring = false; 
                if (_commandInput.isFiring) CreateAmmo(_ammunitionAgent.CurrentAmmunitionPrefab);
            }
        }

        public void FireViaPool()
        {
            if (!_commandInput.weaponLock)
            {
                if (!Cooldown(_ammoCooldownStart, _ammunitionAgent.CurrentAmmunitionBehavior.ammoCooldown, out _ammoCooldownStart)) _commandInput.isFiring = false;
                if (_commandInput.isFiring) _viewServices.Instantiate<Rigidbody2D>(_ammunitionAgent.CurrentAmmunitionPrefab);
            }
        }

        public void Hit(float incomingDamage)
        {
            currentHealth -= incomingDamage;

            if (currentHealth <= 0)
            {
                _agentInitializer.SetActive(false);
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

        private GameObject CreateAmmo(GameObject ammoPrefab)
        {
            _commandInput.isFiring = false;
            return Object.Instantiate(ammoPrefab, _ammoStartTransform);
        }

        public bool Cooldown(float cooldownStart, float cooldown, out float newCooldownStart)
        {
            if (Time.time < cooldownStart + cooldown)
            {
                newCooldownStart = cooldownStart;
                return false;
            }
            newCooldownStart = Time.time;
            return true;
        }
    }
}