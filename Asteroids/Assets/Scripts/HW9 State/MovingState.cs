using UnityEngine;

namespace RRRStudyProject
{
    public abstract class MovingState
    {
        protected float _speed;
        protected Rigidbody2D _rigidbody;

        protected MovingState(float speed, Rigidbody2D rigidbody)
        {
            _speed = speed;
            _rigidbody = rigidbody;
        }

        public abstract void Handle(MovingStateMashine stateMashine);
    }
}