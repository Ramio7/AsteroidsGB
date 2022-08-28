using System.IO;
using System.Text;
using UnityEngine;

namespace RRRStudyProject
{
    public abstract class GameObjectFactory<FactoryType> : ILogCreatedObject
    {
        protected readonly GamePrefabs _gamePrefabs = Object.FindObjectOfType<GamePrefabs>();

        public abstract GameObject Create(GameObject prefab, string objectType, Vector2 startPosition, float maxHealth,
            float speed, float rotationSpeed, float mass);
        protected abstract void AddPhysics(GameObject gameObject, float mass);
        protected abstract FactoryType AddBehaviorScript(GameObject scriptRecipient, string objectType);
        protected abstract void InitializeData(GameObject dataInitializer, FactoryType behaiviorScript, float unitMaxHealth, float unitSpeed, float unitRotationSpeed);

        public void LogCreatedObject(GameObject gameObject)
        {
            string savePath = Path.Combine(Application.dataPath, $"Data/CreatedObjectsLog_{System.DateTime.Now.ToShortDateString()}.txt");

            if (gameObject.TryGetComponent(out FactoryType type))
            {
                string objectInfo = $"{System.DateTime.Now}: {type.GetType().Name} created at coordinates {gameObject.transform.position}\n";

                if (!File.Exists(savePath)) File.Create(savePath);

                using (FileStream fileStream = new FileStream(savePath, FileMode.Append))
                {
                    var objectInfoBytes = Encoding.UTF8.GetBytes(objectInfo);
                    fileStream.Write(objectInfoBytes, 0, objectInfoBytes.Length);
                    fileStream.Close();
                }
            }
            else
                throw new System.Exception("No behavior script found");
        }
    }
}