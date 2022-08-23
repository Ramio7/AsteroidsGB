using UnityEngine;

namespace RRRStudyProject
{
    public class MainGame : MonoBehaviour
    {
        public ListExecuteObject interactiveObjects;
        public ViewServices viewServices;
        public UserInput userInput;
        public MessageBroker messageBroker;

        public UI GameUI;
        public GameOverlay gameOverlay;

        [SerializeField] GamePrefabs gamePrefabs;

        Player player;
        GameObject playerObject;
        UILayer[] uILayers;

        public Factories gameFactories;

        void Awake()
        {
            viewServices = new ViewServices();
            gameFactories = new Factories();
            interactiveObjects = new ListExecuteObject();
            userInput = new UserInput(null);
            gamePrefabs = FindObjectOfType<GamePrefabs>();

            playerObject = gameFactories.unitFactory.CreatePlayerInterceptor(new Vector2(0, 0));
            player = playerObject.GetComponent<Player>();

            uILayers = new UILayer[]
            {
                new PauseMenu(gamePrefabs.layers[0]),
                gameOverlay = new GameOverlay(gamePrefabs.layers[1], player),
                new MainMenu(gamePrefabs.layers[2])
            };
            GameUI = new UI(userInput, uILayers);

            for (int i = 0; i < 10; i++)
            {
                gameFactories.unitFactory.CreateInterceptor(GetPlayerData.GenerateObjectStartPosition(), "Enemy");
                gameFactories.spaceObjectFactory.Create(gamePrefabs.spaceObjectsPrefabs[0], "Asteroid", GetPlayerData.GenerateObjectStartPosition(), Random.Range(10, 1500),
                    Random.Range(0, 150), 0, Random.Range(1, 700));
            }
        }

        private void Start()
        {
            new MainCamera(interactiveObjects, playerObject);
            var root = new Modifier(player);
            root.Add(new DamageModifier(player, 10));
            root.Add(new HealthModifier(player, 200));
            root.Handle();
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