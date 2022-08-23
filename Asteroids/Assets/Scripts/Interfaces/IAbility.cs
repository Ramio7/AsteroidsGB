namespace RRRStudyProject
{
    public interface IAbility
    {
        string Name { get; }
        float Damage { get; }
        Target Target { get; }
        DamageType DamageType { get; }
    }
}