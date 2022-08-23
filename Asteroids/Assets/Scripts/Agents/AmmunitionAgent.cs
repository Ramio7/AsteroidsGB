using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class AmmunitionAgent : IDoDamage, IHaveCooldowns
    {
        private readonly GameObject _agentInitializer;

        private float _ammoCooldownStart;

        private readonly CommandInput _commandInput;

        private readonly IViewServices _viewServices;

        public AmmunitionFactory ammunitionFactory = new AmmunitionFactory();

        public ListAmmunition ammoList = new ListAmmunition();

        public AmmunitionAgent(GameObject agentInitializer)
        {
            _agentInitializer = agentInitializer;

            _viewServices = Object.FindObjectOfType<MainGame>().viewServices;

            if (_viewServices == null) throw new Exception("No ViewServices found on scene");

            if (_agentInitializer.TryGetComponent(out ITakeCommands commandInput))
            {
                _commandInput = commandInput.CommandInput;
            }
            else throw new Exception("No command input found. Add one, please");

            ammunitionFactory.ammoStartTransfrom = _agentInitializer.transform.Find("AmmoStartPosition");
            if (ammunitionFactory.ammoStartTransfrom == null) throw new Exception("Add empty 'AmmoStartPosition' to object or object prefab");

            _ammoCooldownStart = Time.time;
        }

        #region Firing

        public void Fire()
        {
            if (!_commandInput.weaponLock)
            {
                if (!Cooldown(_ammoCooldownStart, ammoList.CurrentAmmunitionBehavior.ammoCooldown, out _ammoCooldownStart)) _commandInput.isFiring = false; //refactor code with ListAmmunition
                if (_commandInput.isFiring && ammoList.CurrentAmmunitionAmount > 0)
                {
                    switch (ammoList.CurrentAmmunitionBehavior.ammoType)
                    {
                        case "Bullet":
                            ammunitionFactory.CreateBullet();
                            ammoList.CurrentAmmunitionAmount--;
                            break;
                        case "Rocket":
                            ammunitionFactory.CreateRocket();
                            ammoList.CurrentAmmunitionAmount--;
                            break;
                        case "Missile":
                            ammunitionFactory.CreateMissile();
                            ammoList.CurrentAmmunitionAmount--;
                            break;
                        case "Torpedo":
                            ammunitionFactory.CreateTorpedo();
                            ammoList.CurrentAmmunitionAmount--;
                            break;
                        default: throw new ArgumentOutOfRangeException("No such type of ammo in Fire metod. Add new case in switch.");
                    }
                    _commandInput.isFiring = false;
                }
            }
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
        #endregion
    }
}