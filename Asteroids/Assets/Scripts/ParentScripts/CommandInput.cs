using UnityEngine;

namespace RRRStudyProject
{
    public abstract class CommandInput : IExecute
    {
        protected string inputTypeName;
        public GameObject controlledObject;

        public Vector2 moveTowards;
        public Vector3 lookAt;
        public float rotationAngle;

        public bool isFiring;

        protected Player player;

        protected CommandInput(string inputTypeName, GameObject controlledObject)
        {
            this.inputTypeName = inputTypeName;
            this.controlledObject = controlledObject;
            player = Object.FindObjectOfType<Player>();
        }

        public string InputTypeName { get => inputTypeName; set => inputTypeName = value; }
        protected void GetPlayerPosition(out float xCoordinate, out float yCoordinate)
        {
            if (player == null)
            {
                xCoordinate = 0;
                yCoordinate = 0;
                return;
            }
            xCoordinate = player.transform.position.x + Random.Range(-10, 10);
            yCoordinate = player.transform.position.y + Random.Range(-10, 10);
        }

        public abstract void Update();
    }
}