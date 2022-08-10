using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Interceptor : Unit
    {
        public Interceptor()
        {
            className = "Interceptor";
        }

        public override void OnEnable()
        {
            commandInput = new AIInput(gameObject);
        }

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
            if (CommandInput.isFiring) damageAgent.Fire();
        }

    }
}