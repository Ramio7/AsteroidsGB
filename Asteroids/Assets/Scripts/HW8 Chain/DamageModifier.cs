namespace RRRStudyProject
{
    public class DamageModifier : Modifier
    {
        readonly float _damage;
        AmmunitionAgent _unitAmmunitionAgent;

        public DamageModifier(Unit unit, float damage) : base(unit)
        {
            _damage = damage;
            _unitAmmunitionAgent = unit.AmmunitionAgent;
        }

        public override void Handle()
        {
            if (_damage > 0) _unitAmmunitionAgent.ammunitionFactory.damageMultiplier += _damage / _unitAmmunitionAgent.ammoList.CurrentAmmunitionBehavior.Data.Damage;
            if (_damage < 0) _unitAmmunitionAgent.ammunitionFactory.damageMultiplier -= _damage / _unitAmmunitionAgent.ammoList.CurrentAmmunitionBehavior.Data.Damage;
            base.Handle();
        }
    }
}