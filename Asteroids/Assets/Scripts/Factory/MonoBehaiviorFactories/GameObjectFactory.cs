using UnityEngine;

namespace RRRStudyProject
{
    public abstract class GameObjectFactory<FactoryType>
    {
        protected readonly GamePrefabs _gamePrefabs = Object.FindObjectOfType<GamePrefabs>();

        public abstract GameObject Create(GameObject prefab, string objectType, Vector2 startPosition, float maxHealth,
            float speed, float rotationSpeed, float mass);
        protected abstract void AddPhysics(GameObject gameObject, float mass);
        protected abstract FactoryType AddBehaviorScript(GameObject scriptRecipient, string objectType);
        protected abstract void InitializeData(GameObject dataInitializer, FactoryType behaiviorScript, float unitMaxHealth, float unitSpeed, float unitRotationSpeed);
    }
}