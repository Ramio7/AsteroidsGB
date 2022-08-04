using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RRRStudyProject
{
    public struct SpaceObjectData : IData, ICollisionDamage
    {
        private readonly string savePath;

        [Header("Обязательные поля")]
        public string name;
        private float maxHealth;
        public float currentHealth;
        private float speed;
        private float rotationSpeed;
        private bool isDead;

        [Header("Автозаполняемые поля")]
        private int spaceObjectId;
        public SVector3 spaceObjectPosition;
        public SQuaternion spaceObjectRotation;
        private float collisionDamage;

        #region struct properties
        public string DataType => "SpaceObject";
        public float Damage { get => collisionDamage; set => collisionDamage = value; }
        public float Health { get => currentHealth; set => currentHealth = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Speed { get => speed; set => speed = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public float CollisionDamage { get => collisionDamage; set => collisionDamage = value; }
        public int ID { get => spaceObjectId; set => spaceObjectId = value; }
        #endregion

        public SpaceObjectData(string spaceObjectname, float spaceObjectmaxHealth, float spaceObjectspeed, float spaceObjectrotationSpeed, GameObject spaceGameObject)
        {
            spaceObjectId = spaceGameObject.GetInstanceID();

            savePath = Path.Combine(Application.dataPath, $"Data/SpaceObjectData{spaceObjectId}.xml");

            name = spaceObjectname;
            maxHealth = spaceObjectmaxHealth;
            currentHealth = maxHealth;
            speed = spaceObjectspeed;
            rotationSpeed = spaceObjectrotationSpeed;
            isDead = false;

            spaceObjectPosition = spaceGameObject.transform.position;
            spaceObjectRotation = spaceGameObject.transform.rotation;

            collisionDamage = (spaceGameObject.TryGetComponent(out Rigidbody2D rigidbody))
                ? rigidbody.mass * speed
                : 0;
        }

        public IData DataConstructor(GameObject spaceObjectGameObject)
        {
            if (spaceObjectGameObject.TryGetComponent(out SpaceObject dataClass))
            {
                return dataClass.Data;
            }
            return null;
        }

        public void Load(GameObject spaceObjectGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(UnitData));

            SpaceObjectData gameObjectData;
            if (spaceObjectGameObject.TryGetComponent(out SpaceObject dataClass))
            {
                gameObjectData = (SpaceObjectData)dataClass.Data;
                if (!File.Exists(gameObjectData.savePath)) throw new System.Exception("Data file not found");
                using var _fileStream = new FileStream(gameObjectData.savePath, FileMode.Open);
                gameObjectData = (SpaceObjectData)_serializer.Deserialize(_fileStream);
                bool dataLoaded = true;
                if (dataLoaded == true)
                {
                    spaceObjectGameObject.transform.SetPositionAndRotation(gameObjectData.spaceObjectPosition, gameObjectData.spaceObjectRotation);
                }
            }
        }

        public void Save(GameObject spaceObjectGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(SpaceObjectData));

            SpaceObjectData _tempdata = (SpaceObjectData)DataConstructor(spaceObjectGameObject);

            if (string.IsNullOrEmpty(_tempdata.savePath)) return;
            using var _fileStream = new FileStream(_tempdata.savePath, FileMode.Append);
            _serializer.Serialize(_fileStream, _tempdata);
        }

        public float CalculateCollisionDamage(Rigidbody2D spaceObjectRigitbody)
        {
            return spaceObjectRigitbody.velocity.magnitude * spaceObjectRigitbody.mass;
        }
    }
}