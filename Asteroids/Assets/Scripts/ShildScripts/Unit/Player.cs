namespace RRRStudyProject
{
    public sealed class Player : Unit
    {
        public Player()
        {
            className = "Player";
        }

        public override void OnEnable()
        {
            commandInput = new UserInput(gameObject);
        }

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
            if (CommandInput.isFiring) damageAgent.Fire();
        }
    }
}