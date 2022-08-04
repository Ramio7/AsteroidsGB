namespace RRRStudyProject
{
    public interface IData
    {
        string DataType { get; }
        int ID { get; set; }
        float Damage { get; set; }
        float Health { get; set; }
        float MaxHealth { get; set; }
        float Speed { get; set; }
        float RotationSpeed { get; set; }

        public IData DataConstructor(UnityEngine.GameObject objectWithData);

        public void Save(UnityEngine.GameObject objectWithData);

        public void Load(UnityEngine.GameObject objectToInputData);
    }
}