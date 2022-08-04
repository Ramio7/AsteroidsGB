using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RRRStudyProject
{
    public struct TrapData: IData
    {
        private readonly string savePath;

        [Header("Обязательные поля")]
        public string name;
        private float damage;
        private float maxHealth;
        public float currentHealth;
        private float radiusOfFire;
        private bool isActive;

        [Header("Автозаполняемые поля")]
        private int trapId;
        public SVector3 trapPosition;
        public SQuaternion trapRotation;

        #region struct properties
        public string DataType => "Trap";
        public float Health { get => currentHealth; set => currentHealth = value; } 
        public float Damage { get => damage; set => damage = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float RadiusOfFire { get => radiusOfFire; set => radiusOfFire = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public float Speed { get => 0.0f; set => throw new System.Exception("Traps don't move"); }
        public float RotationSpeed { get => 0.0f; set => throw new System.Exception("Traps don't rotate"); }
        public int ID { get => trapId; set => trapId = value; }

        #endregion

        public TrapData(string trapName, float trapDamage, float trapMaxHealth, float trapRadiusOfFire, GameObject trapGameObject)
        {
            trapId = trapGameObject.GetInstanceID();

            savePath = Path.Combine(Application.dataPath, $"Data/TrapData{trapId}.xml");

            name = trapName;
            damage = trapDamage;
            maxHealth = trapMaxHealth;
            currentHealth = maxHealth;
            radiusOfFire = trapRadiusOfFire;
            isActive = true;

            trapPosition = trapGameObject.transform.position;
            trapRotation = trapGameObject.transform.rotation;
        }

        public IData DataConstructor(GameObject trapGameObject)
        {
            if (trapGameObject.TryGetComponent(out Trap dataClass))
            {
                return dataClass.Data;
            }
            return null;
        }

        public void Save(GameObject trapGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(TrapData));

            TrapData _tempdata = (TrapData)DataConstructor(trapGameObject);

            if (string.IsNullOrEmpty(_tempdata.savePath)) return;
            using var _fileStream = new FileStream(_tempdata.savePath, FileMode.Append);
            _serializer.Serialize(_fileStream, _tempdata);
        }

        public void Load(GameObject trapGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(TrapData));

            TrapData gameObjectData;
            if (trapGameObject.TryGetComponent(out Trap dataClass))
            {
                gameObjectData = (TrapData)dataClass.Data;
                if (!File.Exists(gameObjectData.savePath)) throw new System.Exception("Data file not found");
                using var _fileStream = new FileStream(gameObjectData.savePath, FileMode.Open);
                gameObjectData = (TrapData)_serializer.Deserialize(_fileStream);
                bool dataLoaded = true;
                if (dataLoaded == true)
                {
                    trapGameObject.transform.SetPositionAndRotation(gameObjectData.trapPosition, gameObjectData.trapRotation);
                }
            }
        }
    }
}