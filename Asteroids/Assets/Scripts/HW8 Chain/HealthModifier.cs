namespace RRRStudyProject
{
    public class HealthModifier : Modifier
    {
        readonly float _health;
        DamageAgent _unitDamageAgent;

        public HealthModifier(Unit unit, float health) : base(unit)
        {
            _unitDamageAgent = unit.DamageAgent;
            _health = health;
        }

        public override void Handle()
        {
            _unitDamageAgent.maxHealth += _health;
            base.Handle();
        }
    }
}