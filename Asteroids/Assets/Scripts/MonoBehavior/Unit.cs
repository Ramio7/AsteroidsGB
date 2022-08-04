using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Unit : MonoBehaviour, IDamageInitializer
    {
        //доработать после проектирования фабрики юнитов
        protected UnitData data;
        protected CommandInput commandInput;
        protected MovementAgent movement;
        protected DamageAgent damageAgent;
        protected AmmunitionAgent unitAmmunition;
        protected Ammunition currentAmmunition;
        protected Rigidbody2D tempRigidbody;

        public DamageAgent DamageAgent { get => damageAgent; set => damageAgent = value; }
        public IData Data { get => data; set => data = (UnitData)value; }
        public AmmunitionAgent AmmunitionAgent { get => unitAmmunition; set => unitAmmunition = value; }
        public Ammunition CurrentAmmunition { get => currentAmmunition; set => currentAmmunition = value; }
        public Rigidbody2D TempRigidbody { get => tempRigidbody; set => tempRigidbody = value; }
        public CommandInput CommandInput { get => commandInput; set => commandInput = value; }
        public MovementAgent Movement { get => movement; set => movement = value; }

        public void SetUnitSpeed(float newSpeed)
        {
            data.Speed = newSpeed;
            Movement.MovingSpeed = newSpeed;
        }

        public void SetUnitRotationSpeed(float newRotationSpeed)
        {
            data.RotationSpeed = newRotationSpeed;
            Movement.RotationSpeed = newRotationSpeed;
        }

        protected float CalculateCollisionDamage()
        {
            return tempRigidbody.mass * tempRigidbody.velocity.magnitude * data.Speed;
        }

        public abstract void FixedUpdate();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
                return;
            }
        }
    }
}
