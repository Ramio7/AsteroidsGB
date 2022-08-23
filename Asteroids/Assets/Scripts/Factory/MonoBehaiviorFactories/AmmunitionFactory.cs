using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class AmmunitionFactory : GameObjectFactory<Ammunition>
    {
        public Transform ammoStartTransfrom;

        public float damageMultiplier = 1;

        public override GameObject Create(GameObject ammoPrefab, string ammoType, Vector2 startPosition, float damage, float startingEnergy, float damageMultiplier, float mass)
        {
            GameObject tempAmmo = Object.Instantiate(ammoPrefab, startPosition, Quaternion.identity);

            AddPhysics(tempAmmo, mass, startingEnergy);

            var tempScript = AddBehaviorScript(tempAmmo, ammoType);

            InitializeData(tempAmmo, tempScript, damage, startingEnergy, damageMultiplier);

            return tempAmmo;
        }

        private GameObject Create(GameObject ammoPrefab, string ammoType, Transform startPosition, float damage, float startingEnergy, float damageMultiplier, float mass)
        {
            GameObject tempAmmo = Object.Instantiate(ammoPrefab, startPosition);

            AddPhysics(tempAmmo, mass, startingEnergy);

            var tempScript = AddBehaviorScript(tempAmmo, ammoType);

            InitializeData(tempAmmo, tempScript, damage, startingEnergy, damageMultiplier);

            return tempAmmo;
        }

        public GameObject CreateBullet()
        {
            var tempObject = Create(_gamePrefabs.ammunitionPrefabs[0], "Bullet", ammoStartTransfrom, 10, 3000, damageMultiplier, 1);
            return tempObject;
        }

        public GameObject CreateRocket()
        {
            var tempObject = Create(_gamePrefabs.ammunitionPrefabs[1], "Rocket", ammoStartTransfrom, 30, 1500, damageMultiplier, 1.5f);
            return tempObject;
        }

        public GameObject CreateMissile()
        {
            var tempObject = Create(_gamePrefabs.ammunitionPrefabs[2], "Missile", ammoStartTransfrom, 55, 1250, damageMultiplier, 2f);
            return tempObject;
        }

        public GameObject CreateTorpedo()
        {
            var tempObject = Create(_gamePrefabs.ammunitionPrefabs[3], "Torpedo", ammoStartTransfrom, 90, 500, damageMultiplier, 5f);
            return tempObject;
        }

        protected override Ammunition AddBehaviorScript(GameObject scriptRecipient, string objectType)
        {
            return objectType switch
            {
                "Bullet" => scriptRecipient.AddComponent<Bullet>(),
                "Rocket" => scriptRecipient.AddComponent<Rocket>(),
                "Missile" => scriptRecipient.AddComponent<Missile>(),
                "Torpedo" => scriptRecipient.AddComponent<Torpedo>(),
                _ => throw new ArgumentException("Invalid ammo type"),
            };
        }

        protected override void AddPhysics(GameObject gameObject, float mass)
        {
            if (!gameObject.TryGetComponent(out Collider2D _)) _ = gameObject.AddComponent<PolygonCollider2D>();

            if (!gameObject.TryGetComponent(out Rigidbody2D tempRigidbody)) tempRigidbody = gameObject.AddComponent<Rigidbody2D>();
            tempRigidbody.mass = mass;
            tempRigidbody.gravityScale = 0;
        }

        private void AddPhysics(GameObject gameObject, float mass, float startingEnergy)
        {
            if (!gameObject.TryGetComponent(out Collider2D _)) _ = gameObject.AddComponent<PolygonCollider2D>();

            if (!gameObject.TryGetComponent(out Rigidbody2D tempRigidbody)) tempRigidbody = gameObject.AddComponent<Rigidbody2D>();
            tempRigidbody.mass = mass;
            tempRigidbody.gravityScale = 0;
            tempRigidbody.AddRelativeForce(Vector2.up * startingEnergy);
        }

        protected override void InitializeData(GameObject dataInitializer, Ammunition behaiviorScript, float damage, float startingEnergy, float damageMultiplier)
        {
            behaiviorScript.Data = new AmmunitionData(behaiviorScript.ammoType, startingEnergy, damage * damageMultiplier, dataInitializer);
        }

        public Ammunition StandartBulletBehavior()
        {
            var existingObject = Object.FindObjectOfType<Bullet>();
            if (existingObject != null)
            {
                return existingObject;
            }
            var tempObject = new GameObject("Bullet");
            var tempScript = tempObject.AddComponent<Bullet>();
            InitializeData(tempObject, tempScript, 10, 3000, 1);
            return tempScript;
        }

        public Ammunition StandartRocketBehavior()
        {
            var existingObject = Object.FindObjectOfType<Rocket>();
            if (existingObject != null)
            {
                return existingObject;
            }
            var tempObject = new GameObject("Rocket");
            var tempScript = tempObject.AddComponent<Rocket>();
            InitializeData(tempObject, tempScript, 30, 1500, 1);
            return tempScript;
        }

        public Ammunition StandartMissileBehavior()
        {
            var existingObject = Object.FindObjectOfType<Missile>();
            if (existingObject != null)
            {
                return existingObject;
            }
            var tempObject = new GameObject("Missile");
            var tempScript = tempObject.AddComponent<Missile>();
            InitializeData(tempObject, tempScript, 55, 1250, 1);
            return tempScript;
        }

        public Ammunition StandartTorpedoBehavior()
        {
            var existingObject = Object.FindObjectOfType<Torpedo>();
            if (existingObject != null)
            {
                return existingObject;
            }
            var tempObject = new GameObject("Torpedo");
            var tempScript = tempObject.AddComponent<Torpedo>();
            InitializeData(tempObject, tempScript, 90, 500, 1);
            return tempScript;
        }
    }
}