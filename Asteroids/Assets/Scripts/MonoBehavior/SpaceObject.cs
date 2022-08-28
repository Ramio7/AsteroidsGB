using UnityEngine;

namespace RRRStudyProject
{
    public abstract class SpaceObject : MonoBehaviour, IDamageInitializer, ITakeCommands, IGameObject
    {
        public string className;
        protected SpaceObjectData data;
        protected MovementAgent movement;
        protected DamageAgent damageAgent;
        protected CommandInput commandInput;

        public DamageAgent DamageAgent { get => damageAgent; set => damageAgent = value; }
        public IData Data { get => data; set => data = (SpaceObjectData)value; }
        public CommandInput CommandInput { get => commandInput; set => commandInput = value; }
        public MovementAgent Movement { get => movement; set => movement = value; }

        public void SetObjectSpeed(float newSpeed)
        {
            Data.Speed = newSpeed;
            Movement.MovingSpeed = newSpeed;
        }

        public void SetObjectRotationSpeed(float newRotationSpeed)
        {
            data.RotationSpeed = newRotationSpeed;
            Movement.RotationSpeed = newRotationSpeed;
        }

        public abstract void OnEnable();

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
                return;
            }
            Debug.Log("Didn't dealt any damage");
        }
    }
}