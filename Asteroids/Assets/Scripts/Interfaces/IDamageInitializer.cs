namespace RRRStudyProject
{
    public interface IDamageInitializer
    {
        IData Data { get; set; }
        DamageAgent DamageAgent { get; set; }
    }
}