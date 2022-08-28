using UnityEngine;

namespace RRRStudyProject
{
    public class StateMashineInput : CommandInput
    {
        public MovingStateMashine stateMashine;

        public StateMashineInput(GameObject controlledObject) : base(controlledObject)
        {
            inputType = "stateMashine";
            stateMashine = new MovingStateMashine(controlledObject, 10, 2);
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                stateMashine.State = new MoveForward(stateMashine.speed, stateMashine.rigidbody);
                stateMashine.Request();
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                stateMashine.State = new MoveBackward(stateMashine.speed, stateMashine.rigidbody);
                stateMashine.Request();
                return;
            }
            if (Input.GetKey(KeyCode.A))
            {
                stateMashine.State = new MoveLeft(stateMashine.speed, stateMashine.rigidbody);
                stateMashine.Request();
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                stateMashine.State = new MoveRight(stateMashine.speed, stateMashine.rigidbody);
                stateMashine.Request();
                return;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                stateMashine.State = new SpeedBoost(stateMashine.speed, stateMashine.rigidbody, stateMashine.speedBoostMultiplyer);
                stateMashine.Request();
                return;
            }
            stateMashine.State = new Idle(stateMashine.speed, stateMashine.rigidbody);
            stateMashine.Request();
        }
    }
}