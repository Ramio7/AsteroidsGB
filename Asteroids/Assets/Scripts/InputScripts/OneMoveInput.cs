using UnityEngine;

namespace RRRStudyProject
{
    public sealed class OneMoveInput : CommandInput
    {
        public OneMoveInput(string inputTypeName, GameObject controlledObject, Vector2 oneMoveDestination) : base(inputTypeName, controlledObject)
        {
            destination = oneMoveDestination;
        }

        private Vector2 destination;

        public Vector2 Destination { get => destination; set => destination = value; }

        public override void Update()
        {
            moveTowards = destination - (Vector2)controlledObject.transform.position;
            rotationAngle = Random.Range(-360, 360);
        }
    }
}
