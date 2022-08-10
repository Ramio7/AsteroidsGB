namespace RRRStudyProject
{
    public class WeaponLocker
    {
        public WeaponLocker(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public bool IsLocked { get; set; }
    }
}