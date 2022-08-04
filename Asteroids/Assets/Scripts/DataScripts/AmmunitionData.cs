using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace RRRStudyProject
{
    public class AmmunitionData : IData
    {
        private readonly string savePath;

        private string name;
        private float health = 1;
        private float startingEnergy;
        private float damage;

        private int ammoId;
        private SVector3 ammoPosition;
        private SQuaternion ammoRotation;

        #region struct properties
        public string DataType => "Ammo";
        public float StartingEnergy { get => startingEnergy; set => startingEnergy = value; }
        public float Damage { get => damage; set => damage = value; }
        public float Health { get => health; set => health = value; }
        public float MaxHealth { get => health; set => health = 1; }
        public float Speed { get => startingEnergy; set => startingEnergy = value; }
        public float RotationSpeed { get => 0.0f; set => throw new System.Exception("Ammunition don't rotate"); }
        public string Name { get => name; set => name = value; }
        public int ID { get => ammoId; set => ammoId = value; }
        #endregion

        public AmmunitionData(string ammoName, float ammoStartingEnergy, float ammoDamage, GameObject ammoGameObject)
        {
            ammoId = ammoGameObject.GetInstanceID();

            savePath = Path.Combine(Application.dataPath, $"Data/AmmoData{ammoId}.xml");

            Name = ammoName;
            startingEnergy = ammoStartingEnergy;
            damage = ammoDamage;
                        
            ammoPosition = ammoGameObject.transform.position;
            ammoRotation = ammoGameObject.transform.rotation;
        }

        public AmmunitionData(string ammoName)
        {
            Name = ammoName;
        }

        public IData DataConstructor(GameObject ammoGameObject)
        {
            if (ammoGameObject.TryGetComponent(out Ammunition dataClass))
            {
                return dataClass.data;
            }
            return null;
        }

        public void Load(GameObject ammoGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(AmmunitionData));

            AmmunitionData gameObjectData;
            if (ammoGameObject.TryGetComponent(out Ammunition dataClass))
            {
                gameObjectData = dataClass.data;
                if (!File.Exists(gameObjectData.savePath)) throw new System.Exception("Data file not found");
                using var _fileStream = new FileStream(gameObjectData.savePath, FileMode.Open);
                gameObjectData = (AmmunitionData)_serializer.Deserialize(_fileStream);
                bool dataLoaded = true;
                if (dataLoaded == true)
                {
                    ammoGameObject.transform.SetPositionAndRotation(gameObjectData.ammoPosition, gameObjectData.ammoRotation);
                }
            }
        }

        public void Save(GameObject ammoGameObject)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(AmmunitionData));

            AmmunitionData _tempdata = (AmmunitionData)DataConstructor(ammoGameObject);

            if (string.IsNullOrEmpty(_tempdata.savePath)) return;
            using var _fileStream = new FileStream(_tempdata.savePath, FileMode.Append);
            _serializer.Serialize(_fileStream, _tempdata);
        }
    }
}