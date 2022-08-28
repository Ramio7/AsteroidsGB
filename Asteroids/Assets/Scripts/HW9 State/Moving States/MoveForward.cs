using UnityEngine;

namespace RRRStudyProject
{
    public class MoveForward : MovingState
    {
        public MoveForward(float speed, Rigidbody2D rigidbody) : base(speed, rigidbody)
        {
        }

        public override void Handle(MovingStateMashine stateMashine)
        {
            _rigidbody.AddRelativeForce(_speed * Time.deltaTime * Vector2.up);
        }
    }
}