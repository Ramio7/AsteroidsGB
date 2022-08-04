using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Player : Unit
    {
        public string className = "Player";
        public float playerSpeed = 15;
        public float playerMaxHealth = 100;
        public float playerRotationSpeed = 6;

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = CalculateCollisionDamage();
            if (CommandInput.isFiring) damageAgent.Fire();
        }
    }
}