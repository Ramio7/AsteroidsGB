namespace RRRStudyProject.HW6
{
    public class TestUnit : ITestUnitData
    {
        protected string _unitType = "Unit";
        protected int _unitHealth = 1;
        public string Type { get => _unitType; set => _unitType = value; }
        public int Health { get => _unitHealth; set => _unitHealth = value; }
    }
}