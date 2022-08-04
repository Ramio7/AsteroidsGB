using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RRRStudyProject
{
    public struct UnitData: IData
    {
        private readonly string savePath;

        [Header("Обязательные поля")]
        public string name;
        private float maxHealth;
        private float currentHealth;
        private float speed;
        private float rotationSpeed;
        private bool isDead;

        [Header("Автозаполняемые поля")]
        private int unitId;
        private float collisionDamage;
        public SVector3 unitPosition;
        public SQuaternion unitRotation;

        #region struct properties
        public string DataType => "Unit";
        public float Health { get => currentHealth; set => currentHealth = value; }
        public float Speed { get => speed; set => speed = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public int ID { get => unitId; set => unitId = value; }
        public float Damage { get => collisionDamage; set => collisionDamage = value; }
        #endregion

        public UnitData(string unitName, float unitMaxHealth, float unitSpeed, float unitRotationSpeed, GameObject unitGameObject)
        {
            unitId = unitGameObject.GetInstanceID();

            savePath = Path.Combine(Application.dataPath, $"Data/UnitData{unitId}.xml");

            name = unitName;
            maxHealth = unitMaxHealth;
            currentHealth = maxHealth;
            speed = unitSpeed;
            rotationSpeed = unitRotationSpeed;
            isDead = false;

            unitPosition = unitGameObject.transform.position;
            unitRotation = unitGameObject.transform.rotation;

            collisionDamage = (unitGameObject.TryGetComponent(out Rigidbody2D rigidbody))
                ? rigidbody.mass * speed
                : 0;
        }

        public IData DataConstructor(GameObject unitGameObject)
        {
            if (unitGameObject.TryGetComponent(out Unit dataClass))
            {
                return dataClass.Data;
            }
            return null;
        }

        public void Load(GameObject unitGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(UnitData));

            UnitData gameObjectData;
            if (unitGameObject.TryGetComponent(out Unit dataClass))
            {
                gameObjectData = (UnitData)dataClass.Data;
                if (!File.Exists(gameObjectData.savePath)) throw new System.Exception("Data file not found");
                using var _fileStream = new FileStream(gameObjectData.savePath, FileMode.Open);
                gameObjectData = (UnitData)_serializer.Deserialize(_fileStream);
                bool dataLoaded = true;
                if (dataLoaded == true)
                {
                    unitGameObject.transform.SetPositionAndRotation(gameObjectData.unitPosition, gameObjectData.unitRotation);
                }
            }
        }

        public void Save(GameObject unitGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(UnitData));

            UnitData _tempdata = (UnitData)DataConstructor(unitGameObject);

            if (string.IsNullOrEmpty(_tempdata.savePath)) return;
            using var _fileStream = new FileStream(_tempdata.savePath, FileMode.Create);
            _serializer.Serialize(_fileStream, _tempdata);
        }
    }
}