using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Interceptor : Unit
    {
        public override void OnEnable()
        {
            className = "Interceptor";
            commandInput = new AIInput(gameObject);
        }

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
            if (CommandInput.isFiring) unitAmmunition.Fire();
        }
    }
}