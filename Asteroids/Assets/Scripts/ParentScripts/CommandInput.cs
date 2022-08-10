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
        public string inputType;

        protected Player player;

        protected CommandInput(GameObject controlledObject)
        {
            this.controlledObject = controlledObject;
            player = Object.FindObjectOfType<Player>();
            MainGame _main = Object.FindObjectOfType<MainGame>();
            if (_main == null) throw new System.Exception("No main game script found");
            if (_main.interactiveObjects == null) throw new System.Exception("Add ListExecuteObject to main game script");
            _main.interactiveObjects.AddExecuteObj(this);
        }

        public abstract void Update();
    }
}