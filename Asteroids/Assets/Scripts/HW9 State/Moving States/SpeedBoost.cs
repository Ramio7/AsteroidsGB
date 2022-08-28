using UnityEngine;

namespace RRRStudyProject
{
    public class SpeedBoost : MovingState
    {
        public float SpeedBoostMultiplyer { get; private set; }

        public SpeedBoost(float speed, Rigidbody2D rigidbody, float speedBoostMultiplyer) : base(speed, rigidbody)
        {
            SpeedBoostMultiplyer = speedBoostMultiplyer;
        }

        public override void Handle(MovingStateMashine stateMashine)
        {
            _rigidbody.AddRelativeForce(SpeedBoostMultiplyer * _speed * _rigidbody.velocity.normalized);
        }
    }
}