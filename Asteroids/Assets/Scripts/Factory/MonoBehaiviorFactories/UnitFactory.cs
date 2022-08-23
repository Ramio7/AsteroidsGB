using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public sealed class UnitFactory : GameObjectFactory<Unit>
    {
        public override GameObject Create(GameObject unitPrefab, string unitType, Vector2 unitStartPosition, float unitMaxHealth, 
            float unitSpeed, float unitRotationSpeed, float unitMass)
        {
            GameObject tempUnit = Object.Instantiate(unitPrefab, unitStartPosition, Quaternion.identity);

            AddPhysics(tempUnit, unitMass);

            var tempScript = AddBehaviorScript(tempUnit, unitType);

            InitializeData(tempUnit, tempScript, unitMaxHealth, unitSpeed, unitRotationSpeed);
            
            return tempUnit;
        }

        protected override void AddPhysics(GameObject gameObject, float mass)
        {
            if (!gameObject.TryGetComponent(out Collider2D _)) _ = gameObject.AddComponent<PolygonCollider2D>();
            
            if (!gameObject.TryGetComponent(out Rigidbody2D tempRigidbody)) tempRigidbody = gameObject.AddComponent<Rigidbody2D>();
            tempRigidbody.useAutoMass = true;
            tempRigidbody.gravityScale = 0;
        }

        protected override Unit AddBehaviorScript(GameObject scriptRecipient, string objectType)
        {
            return objectType switch
            {
                "Player" => scriptRecipient.AddComponent<Player>(),
                "Interceptor" => scriptRecipient.AddComponent<Interceptor>(),
                _ => throw new System.Exception("Invalid object type"),
            };
        }

        protected override void InitializeData(GameObject dataInitializer, Unit behaiviorScript, float unitMaxHealth, float unitSpeed, float unitRotationSpeed)
        {
            behaiviorScript.Data = new UnitData(behaiviorScript.className, unitMaxHealth, unitSpeed, unitRotationSpeed, dataInitializer);
            behaiviorScript.AmmunitionAgent = new AmmunitionAgent(dataInitializer);
            behaiviorScript.DamageAgent = new DamageAgent(dataInitializer);
            behaiviorScript.Movement = new MovementAgent(dataInitializer, behaiviorScript.Data, behaiviorScript.CommandInput);
            List<IAbility> abilities = new List<IAbility>
            {
                new Ability("Repair", 30, Target.Self, DamageType.Heal),
                new Ability("Rocket Barage", 210, Target.MultiObject, DamageType.Explosive),
                new Ability("Gravitational collapse", 50, Target.Area, DamageType.Kinetic),
                new Ability("Focus Fire", 200, Target.SingleObject, DamageType.Thermal),
            };
            behaiviorScript.Abilities = new ListAbility(abilities, behaiviorScript.CommandInput);
        }

        public GameObject CreatePlayerInterceptor(Vector2 playerStartPosition)
        {
            GameObject player = Object.Instantiate(_gamePrefabs.playerShipPrefabs[0], playerStartPosition, Quaternion.identity);

            AddPhysics(player, 0);

            Player playerScript = (Player)AddBehaviorScript(player, "Player");

            InitializeData(player, playerScript, 100, 30, 10);

            player.AddComponent<TimeBody>();

            return player;
        }

        public GameObject CreatePlayerFrigate(Vector2 playerStartPosition)
        {
            GameObject player = Object.Instantiate(_gamePrefabs.playerShipPrefabs[1], playerStartPosition, Quaternion.identity);

            AddPhysics(player, 0);

            Player playerScript = (Player)AddBehaviorScript(player, "Player");

            InitializeData(player, playerScript, 175, 20, 8);

            player.AddComponent<TimeBody>();

            return player;
        }

        public GameObject CreatePlayerDestroyer(Vector2 playerStartPosition)
        {
            GameObject player = Object.Instantiate(_gamePrefabs.playerShipPrefabs[2], playerStartPosition, Quaternion.identity);

            AddPhysics(player, 0);

            Player playerScript = (Player)AddBehaviorScript(player, "Player");

            InitializeData(player, playerScript, 400, 7.5f, 3);

            player.AddComponent<TimeBody>();

            return player;
        }

        public GameObject CreateInterceptor(Vector2 unitStartPosition, string unitToPlayerRelations)
        {
            GameObject tempUnit = unitToPlayerRelations switch
            {
                "Enemy" => Object.Instantiate(_gamePrefabs.enemyShipPrefabs[0], unitStartPosition, Quaternion.identity),
                "Ally" => Object.Instantiate(_gamePrefabs.allyShipPrefabs[0], unitStartPosition, Quaternion.identity),
                "Neutral" => Object.Instantiate(_gamePrefabs.neutralShipPrefabs[0], unitStartPosition, Quaternion.identity),
                _ => throw new System.Exception("Invalid unit to player relations tag"),
            };

            AddPhysics(tempUnit, 0);

            var tempScript = AddBehaviorScript(tempUnit, "Interceptor");

            InitializeData(tempUnit, tempScript, 100, 30, 10);

            return tempUnit;
        }
    }
}