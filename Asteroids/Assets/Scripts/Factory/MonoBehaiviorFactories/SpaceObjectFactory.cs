using UnityEngine;

namespace RRRStudyProject
{
    public sealed class SpaceObjectFactory : GameObjectFactory<SpaceObject>
    {
        public override GameObject Create(GameObject prefab, string spaceObjectType, Vector2 startPosition, float maxHealth, float speed, float rotationSpeed, float mass)
        {
            GameObject tempSpaceObject = Object.Instantiate(prefab, startPosition, Quaternion.identity);

            AddPhysics(tempSpaceObject, mass);

            var tempScript = AddBehaviorScript(tempSpaceObject, spaceObjectType);

            InitializeData(tempSpaceObject, tempScript, maxHealth, speed, rotationSpeed);

            return tempSpaceObject;
        }

        protected override void AddPhysics(GameObject gameObject, float mass)
        {
            if (!gameObject.TryGetComponent(out Rigidbody2D tempRigidbody)) tempRigidbody = gameObject.AddComponent<Rigidbody2D>();
            tempRigidbody.mass = mass;
            tempRigidbody.gravityScale = 0;
            if (!gameObject.GetComponentInChildren<Collider2D>()) gameObject.AddComponent<PolygonCollider2D>();
        }

        protected override SpaceObject AddBehaviorScript(GameObject scriptRecipient, string objectType)
        {
            return scriptRecipient.AddComponent<Asteroid>();
        }

        protected override void InitializeData(GameObject dataInitializer, SpaceObject behaiviorScript, float maxHealth, float speed, float rotationSpeed)
        {
            behaiviorScript.Data = new SpaceObjectData(behaiviorScript.className, maxHealth, speed, rotationSpeed, dataInitializer);
            behaiviorScript.DamageAgent = new DamageAgent(dataInitializer);
            behaiviorScript.Movement = new MovementAgent(dataInitializer, behaiviorScript.Data, behaiviorScript.CommandInput);
        }
    }
}