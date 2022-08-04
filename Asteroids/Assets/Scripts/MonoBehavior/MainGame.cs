using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class MainGame : MonoBehaviour
    {
        private GameObjectFactory _factory;

        public ListExecuteObject interactiveObjects;
        public GamePrefabs gamePrefabs;
        public UI GameUI;

        public GameObject playerObject;
        public ViewServices viewServices;

        public DictionarySurrogated dictionary;

        void Awake()
        {
            viewServices = new ViewServices();
            gamePrefabs = FindObjectOfType<GamePrefabs>();
            _factory = new GameObjectFactory();
            interactiveObjects = new ListExecuteObject();
            if (!FindObjectOfType<Player>())
            {
                _factory.CreateGameObject("Player", gamePrefabs.playerShipPrefabs[0]);
                playerObject = GameObject.FindGameObjectWithTag("Player");
            }
            if (!FindObjectOfType<Interceptor>()) _factory.CreateGameObject("Interceptor", gamePrefabs.enemyShipPrefabs[0]);
            if (!FindObjectOfType<Asteroid>()) _factory.CreateGameObject("Asteroid", gamePrefabs.spaceObjectsPrefabs[0]);
        }

        private void Start()
        {
            GameUI = new UI(playerObject);
            dictionary = new DictionarySurrogated(viewServices.ViewCache);
        }

        void Update()
        {
            if (!FindObjectOfType<Player>())
            {
                _factory.CreateGameObject("Player", gamePrefabs.playerShipPrefabs[0]);
                playerObject = GameObject.FindGameObjectWithTag("Player");
            }
            for (int i = 0; i < interactiveObjects.Length; i++)
            {
                if (interactiveObjects[i] == null)
                {
                    continue;
                }

                interactiveObjects[i].Update();
            }
        }
    }
}