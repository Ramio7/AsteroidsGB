using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Interceptor : Unit
    {
        public string className = "Interceptor";
        public float interceptorSpeed = 15;
        public float interceptorMaxHealth = 100;
        public float interceptorRotationSpeed = 6;

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = CalculateCollisionDamage();
            if (CommandInput.isFiring) damageAgent.Fire();
        }
    }
}