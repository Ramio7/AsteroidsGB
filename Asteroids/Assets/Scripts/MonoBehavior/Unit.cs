using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Unit : MonoBehaviour, IDamageInitializer, ICarryWeapons, ITakeCommands, IGameObject
    {
        public string className;
        protected UnitData data;
        protected CommandInput commandInput;
        protected MovementAgent movement;
        protected DamageAgent damageAgent;
        protected AmmunitionAgent unitAmmunition;
        protected ListAbility abilities;

        public DamageAgent DamageAgent { get => damageAgent; set => damageAgent = value; }
        public IData Data { get => data; set => data = (UnitData)value; }
        public AmmunitionAgent AmmunitionAgent { get => unitAmmunition; set => unitAmmunition = value; }
        public CommandInput CommandInput { get => commandInput; set => commandInput = value; }
        public MovementAgent Movement { get => movement; set => movement = value; }
        public ListAbility Abilities { get => abilities; set => abilities = value; }

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

        public abstract void FixedUpdate();

        public abstract void OnEnable();

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
            }
        }
    }
}
