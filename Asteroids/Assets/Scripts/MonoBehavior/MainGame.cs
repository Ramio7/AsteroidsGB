using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class MainGame : MonoBehaviour
    {
        public ListExecuteObject interactiveObjects;
        public GamePrefabs gamePrefabs;
        public UI GameUI;

        public GameObject playerObject;
        public ViewServices viewServices;

        public DictionarySurrogated dictionary;

        Factories gameFactories;

        void Awake()
        {
            viewServices = new ViewServices();
            gameFactories = new Factories();
            interactiveObjects = new ListExecuteObject();
            gamePrefabs = FindObjectOfType<GamePrefabs>();

            playerObject = gameFactories.unitFactory.CreatePlayerInterceptor(new Vector2(0, 0));
            gameFactories.unitFactory.CreateInterceptor(GetPlayerData.GenerateObjectStartPosition(), "Enemy");
            gameFactories.spaceObjectFactory.Create(gamePrefabs.spaceObjectsPrefabs[0], "Asteroid", GetPlayerData.GenerateObjectStartPosition(), Random.Range(10, 1500),
                Random.Range(0, 150), 0, Random.Range(1, 700));
        }

        private void Start()
        {
            GameUI = new UI(playerObject);
            dictionary = new DictionarySurrogated(viewServices.ViewCache);
        }

        void Update()
        {
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