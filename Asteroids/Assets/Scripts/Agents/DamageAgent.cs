using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class DamageAgent : ITakeDamage, IDoDamage, IHeal, IHaveCooldowns //вывести дополнительные методы когда будет время, отрефакторить код. Убрать лишние переменные от 
        //родительского объекта. Оставить только урон, здоровье и ввести текущее здоровье.
    {
        private readonly MainGame _mainGame;

        private float _ammoCooldownStart;

        private readonly GamePrefabs _gamePrefabs;

        private readonly GameObject _agentInitializer;
        private readonly AmmunitionAgent _ammunitionAgent;
        private readonly CommandInput _commandInput;
        private readonly IData _initializerData;

        private readonly Ammunition[] _initializerAmmo;
        private readonly AmmunitionData[] _initializerAmmoData;

        private readonly IDamageInitializer _agentClass;
        private readonly Transform _ammoStartTransform;

        public IData InitializerData => _initializerData;

        private IViewServices _viewServices;

        //private GameObjectBuilder ammoBuilder = new GameObjectBuilder();

        public DamageAgent(GameObject agentInitializer)
        {
            _mainGame = Object.FindObjectOfType<MainGame>();
            _gamePrefabs = Object.FindObjectOfType<GamePrefabs>();
            _ammoStartTransform = agentInitializer.transform.Find("AmmoStartPosition");
            _agentInitializer = agentInitializer;
            _ammoCooldownStart = Time.time;
            _viewServices = _mainGame.viewServices;

            if (_agentInitializer.TryGetComponent(out IDamageInitializer _tempAgentClass))
            {
                switch (_tempAgentClass.Data.DataType)
                {
                    case "Unit":
                        {
                            Unit _tempUnit = (Unit)_tempAgentClass;
                            _agentClass = _tempAgentClass;
                            _ammunitionAgent = _agentClass.AmmunitionAgent;
                            _initializerData = (UnitData)_tempAgentClass.Data;
                            _initializerAmmo = _ammunitionAgent.ExtractAmmunitionClasses();
                            _initializerAmmoData = ExtractAmmunitionData();
                            _commandInput = _tempUnit.CommandInput;
                            break;
                        }
                    case "Trap":
                        {
                            break;
                        }
                    case "SpaceObject":
                        {
                            SpaceObject _tempSpaceObject = (SpaceObject)_tempAgentClass;
                            _agentClass = _tempAgentClass;
                            _initializerData = (SpaceObjectData)_tempAgentClass.Data;
                            _commandInput = _tempSpaceObject.CommandInput;
                            break;
                        }
                    default: throw new Exception("Unknown object class");
                }
            }
            else throw new Exception("DamageInitializer class not found");
        }

        public void Fire()
        {
            if (!Cooldown(_ammoCooldownStart, 0.5f, out _ammoCooldownStart)) _commandInput.isFiring = false;
            if (_commandInput.isFiring)
            {
                int _currentAmmoIndex = _ammunitionAgent.Index;
                AmmunitionData _tempAmmoData = _initializerAmmoData[_currentAmmoIndex];
                switch (_tempAmmoData.Name)
                {
                    case "Bullet":
                        {
                            CreateAmmo(_gamePrefabs.ammunitionPrefabs[0]);
                            break;
                        }
                    case "Rocket":
                        {
                            CreateAmmo(_gamePrefabs.ammunitionPrefabs[1]);
                            break;
                        }
                    case "Missile":
                        {
                            CreateAmmo(_gamePrefabs.ammunitionPrefabs[2]);
                            break;
                        }
                    case "Torpedo":
                        {
                            CreateAmmo(_gamePrefabs.ammunitionPrefabs[3]);
                            break;
                        }
                    default: throw new Exception("No ammo detected");
                }
            }
        }

        public void FireViaPool()
        {
            if (!Cooldown(_ammoCooldownStart, 0.5f, out _ammoCooldownStart)) _commandInput.isFiring = false;
            if (_commandInput.isFiring)
            {
                _viewServices.Instantiate<Rigidbody2D>(_gamePrefabs.ammunitionPrefabs[_ammunitionAgent.Index]);
            }
        }

        public void Hit(float incomingDamage)
       {
            _initializerData.Health -= incomingDamage;

            if (_initializerData.Health <= 0)
            {
                _agentInitializer.SetActive(false);
            }
        }

        public void Heal(float healDamage)
        {
            _initializerData.Health += healDamage;

            if (_initializerData.Health >= _initializerData.MaxHealth)
            {
                _initializerData.Health = _initializerData.MaxHealth;
            }
        }

        private void CreateAmmo(GameObject ammoPrefab)
        {
            _commandInput.isFiring = false;
            Object.Instantiate(ammoPrefab, _ammoStartTransform);
        }

        //public void CreateAmmoViaObjectBuilder() //учебная функция, волшебные числа для теста работы, для полной работоспособности прикрутить текущие координаты для старта и
        //    //разобраться с багом (создаётся только одна пуля, возможно: надо создавать билдер для каждой пули отдельно)
        //{
        //    _commandInput.isFiring = false;
        //    GameObject _ammo = ammoBuilder.Physics.BoxCollider2D().Rigidbody2D(0.5f).Visual.Name("Bullet");
        //    _ammo.AddComponent<Bullet>();
        //    Rigidbody2D _ammoRigidbody = _ammo.GetComponent<Rigidbody2D>();
        //    _ammoRigidbody.AddRelativeForce(Vector2.up * 100, ForceMode2D.Impulse);
        //}

        private AmmunitionData[] ExtractAmmunitionData()
        {
            AmmunitionData[] result = new AmmunitionData[_initializerAmmo.Length];
            for (int i = 0; i < _initializerAmmo.Length; i++)
            {
                if (_initializerAmmo[i].Data != null)
                {
                    result[i] = (AmmunitionData)_initializerAmmo[i].Data;
                }
            }
            if (result != null) return result;
            throw new System.Exception("No ammo data founded");
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