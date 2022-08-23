namespace RRRStudyProject
{
    public sealed class Player : Unit
    {
        public string playerName = "Default";

        public override void OnEnable()
        {
            className = "Player";
            MainGame mainGame = FindObjectOfType<MainGame>();
            commandInput = mainGame.userInput;
        }

        public override void FixedUpdate()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
            if (CommandInput.isFiring) unitAmmunition.Fire();
        }
    }
}