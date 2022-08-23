using UnityEngine;

namespace RRRStudyProject
{
    public abstract class CommandInput : IExecute
    {
        public GameObject controlledObject;

        public Vector2 moveTowards;
        public Vector3 lookAt;
        public float rotationAngle;

        public bool isFiring;
        public bool weaponLock;
        public bool pauseMenu;
        public bool mainMenu;
        public bool inGame = true;

        [SerializeField] public KeyCommand useFirstAbility = new KeyCommand(KeyCode.Alpha1);
        [SerializeField] public KeyCommand useSecondAbility = new KeyCommand(KeyCode.Alpha2);
        [SerializeField] public KeyCommand useThirdAbility = new KeyCommand(KeyCode.Alpha3);
        [SerializeField] public KeyCommand useFourthAbility = new KeyCommand(KeyCode.Alpha4);

        public string inputType;

        protected CommandInput(GameObject controlledObject)
        {
            this.controlledObject = controlledObject;
            MainGame _main = Object.FindObjectOfType<MainGame>();
            if (_main == null) throw new System.Exception("No main game script found");
            if (_main.interactiveObjects == null) throw new System.Exception("Add ListExecuteObject to main game script");
            _main.interactiveObjects.AddExecuteObj(this);
        }

        public abstract void Update();
    }
}