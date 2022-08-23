namespace RRRStudyProject
{
    public class Ability : IAbility
    {
        public string Name { get; }

        public float Damage { get; }

        public Target Target { get; }

        public DamageType DamageType { get; }

        public Ability(string name, float damage, Target target, DamageType damageType)
        {
            Name = name;
            Damage = damage;
            Target = target;
            DamageType = damageType;
        }
    }
}