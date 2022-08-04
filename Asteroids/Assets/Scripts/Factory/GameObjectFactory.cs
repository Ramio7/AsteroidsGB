using UnityEngine;

namespace RRRStudyProject
{
    public class GameObjectFactory //добавить методы для присвоения свойств объектов, когда будет время; добавить фабрики для разных классов объектов
    {
        private readonly MainGame _game = Object.FindObjectOfType<MainGame>();
        private readonly GamePrefabs _gamePrefabs = Object.FindObjectOfType<GamePrefabs>();
        public void CreateGameObject(string GameObjectType, GameObject createObjectPrefab)
        {
            if (GameObjectType == "Player") 
            {
                GameObject player = Object.Instantiate(createObjectPrefab, Vector3.zero, Quaternion.identity);
                var _tempRigidbody = player.AddComponent<Rigidbody2D>();
                _tempRigidbody.mass = 30;
                _tempRigidbody.gravityScale = 0;
                Player _gameObjectScript = player.AddComponent<Player>();
                _gameObjectScript.TempRigidbody = _tempRigidbody;
                _gameObjectScript.Data = new UnitData(_gameObjectScript.className, _gameObjectScript.playerMaxHealth,
                    _gameObjectScript.playerSpeed, _gameObjectScript.playerRotationSpeed, player);
                _gameObjectScript.CommandInput = new UserInput("UserInput", player);
                _gameObjectScript.Movement = new MovementAgent(player, _gameObjectScript.Data, _gameObjectScript.CommandInput);
                _gameObjectScript.AmmunitionAgent = new AmmunitionAgent
                {
                    Index = 0
                };
                _gameObjectScript.AmmunitionAgent.InitiateAmmunitionList(_gamePrefabs.ammunitionPrefabs);
                _gameObjectScript.DamageAgent = new DamageAgent(player);
                _game.interactiveObjects.AddExecuteObj(_gameObjectScript.CommandInput);
                player.tag = "Player";
                player.name = "Player";
                return;
            }
            
            GenerateObjectStartTransform(out Vector3 objectStartPosition, out Quaternion objectStartRotation);            
            GameObject _tempGameObject = Object.Instantiate(createObjectPrefab, objectStartPosition, objectStartRotation);

            switch (GameObjectType)
            {
                case "Interceptor":
                    {
                        Interceptor _gameObjectScript = _tempGameObject.AddComponent<Interceptor>();
                        var _tempRigidbody = _tempGameObject.AddComponent<Rigidbody2D>();
                        _tempRigidbody.mass = 30;
                        _tempRigidbody.gravityScale = 0;
                        _gameObjectScript.TempRigidbody = _tempRigidbody;
                        _gameObjectScript.Data = new UnitData(_gameObjectScript.className, _gameObjectScript.interceptorMaxHealth,
                            _gameObjectScript.interceptorSpeed, _gameObjectScript.interceptorRotationSpeed, _tempGameObject);
                        _gameObjectScript.CommandInput = new AIInput("AIInput", _tempGameObject);
                        _gameObjectScript.Movement = new MovementAgent(_tempGameObject, _gameObjectScript.Data, _gameObjectScript.CommandInput);
                        _gameObjectScript.AmmunitionAgent = new AmmunitionAgent
                        {
                            Index = 0
                        };
                        _gameObjectScript.AmmunitionAgent.InitiateAmmunitionList(_gamePrefabs.ammunitionPrefabs);
                        _gameObjectScript.DamageAgent = new DamageAgent(_tempGameObject);
                        _game.interactiveObjects.AddExecuteObj(_gameObjectScript.CommandInput);
                        _tempGameObject.tag = "Enemy";
                        _tempGameObject.name = $"Interceptor{_gameObjectScript.Data.ID}";
                        break;
                    }
                case "Asteroid":
                    {
                        Asteroid _gameObjectScript = _tempGameObject.AddComponent<Asteroid>();
                        if (!_tempGameObject.TryGetComponent(out Rigidbody2D _tempRigidbody)) _tempRigidbody = _tempGameObject.AddComponent<Rigidbody2D>();
                        _tempRigidbody.mass = Random.Range(100, 1000);
                        _tempRigidbody.gravityScale = 0;
                        _gameObjectScript.Data = new SpaceObjectData(_gameObjectScript.className, _gameObjectScript.asteroidMaxHealth,
                            _gameObjectScript.asteroidSpeed, _gameObjectScript.asteroidRotationSpeed, _tempGameObject);
                        GetPlayerPosition(out float xCoordinate, out float yCoordinate);
                        xCoordinate += Random.Range(-10, 10);
                        yCoordinate += Random.Range(-10, 10);
                        _gameObjectScript.AsteroidDestination = new Vector3(xCoordinate, yCoordinate);
                        _gameObjectScript.CommandInput = new OneMoveInput("OneMoveInput", _tempGameObject, _gameObjectScript.AsteroidDestination);
                        _gameObjectScript.Movement = new MovementAgent(_tempGameObject, _gameObjectScript.Data, _gameObjectScript.CommandInput);
                        _gameObjectScript.DamageAgent = new DamageAgent(_tempGameObject);
                        _gameObjectScript.TempRigidbody = _tempRigidbody;
                        _game.interactiveObjects.AddExecuteObj(_gameObjectScript.CommandInput);
                        _tempGameObject.tag = "Asteroid";
                        _tempGameObject.name = $"Asteroid{_gameObjectScript.Data.ID}";
                        break;
                    }
            }
        }

        private void GetPlayerPosition(out float xCoordinate, out float yCoordinate)
        {
            if (!Object.FindObjectOfType<Player>())
            {
                xCoordinate = 0;
                yCoordinate = 0;
                return;
            }
            Player _player = Object.FindObjectOfType<Player>();
            Vector2 _playerPosition = _player.transform.position;
            xCoordinate = _playerPosition.x + Random.Range(-10, 10);
            yCoordinate = _playerPosition.y + Random.Range(-10, 10);
        }

        public void GenerateObjectStartTransform(out Vector3 objectStartPosition, out Quaternion objectStartRotation)
        {
            GetPlayerPosition(out float xCoordinate, out float yCoordinate);
            float xAxisPosition = xCoordinate + Random.Range(-100, 100);
            float yAxisPosition = yCoordinate + Random.Range(-100, 100);
            objectStartPosition = new Vector2(xAxisPosition, yAxisPosition);
            objectStartRotation = Quaternion.identity;
        }
    }
}