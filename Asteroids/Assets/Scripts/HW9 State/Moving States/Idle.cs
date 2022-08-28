using UnityEngine;

namespace RRRStudyProject
{
    public class Idle : MovingState
    {
        public Idle(float speed, Rigidbody2D rigidbody) : base(speed, rigidbody)
        {
        }

        public override void Handle(MovingStateMashine stateMashine)
        {
        }
    }
}