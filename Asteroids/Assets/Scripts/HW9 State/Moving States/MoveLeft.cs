using UnityEngine;

namespace RRRStudyProject
{
    public class MoveLeft : MovingState
    {
        public MoveLeft(float speed, Rigidbody2D rigidbody) : base(speed, rigidbody)
        {
        }

        public override void Handle(MovingStateMashine stateMashine)
        {
            _rigidbody.AddRelativeForce(_speed * Time.deltaTime * Vector2.left);
        }
    }
}