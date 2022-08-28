using UnityEngine;

namespace RRRStudyProject
{
    public class MovingStateMashine
    {
        private MovingState _state;
        public Rigidbody2D rigidbody;
        public float speed;
        public float speedBoostMultiplyer;

        public MovingStateMashine(GameObject stateMashineTarget, float speed, float speedBoostMultiplyer)
        {
            if (stateMashineTarget.TryGetComponent(out Rigidbody2D objectRigidbody)) rigidbody = objectRigidbody;
            else throw new System.Exception("No rigidbbody attached to object");
            _state = new Idle(speed, rigidbody);
            this.speed = speed;
            this.speedBoostMultiplyer = speedBoostMultiplyer;
        }

        public MovingState State { set => _state = value; }

        public void Request()
        {
            _state.Handle(this);
        }
    }
}