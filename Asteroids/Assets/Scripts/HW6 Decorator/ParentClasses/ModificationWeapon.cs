namespace RRRStudyProject.HW6.Decorator
{
    internal abstract class ModificationWeapon : IFire
    {
        Weapon _weapon;
        public bool isApplied = false;

        protected abstract Weapon AddModification(Weapon weapon);
        protected abstract Weapon RemoveModification(Weapon weapon);

        public void ApplyModification(Weapon weapon)
        {
            _weapon = AddModification(weapon);
        }

        public void DetachModification(Weapon weapon)
        {
            _weapon = RemoveModification(weapon);
        }

        public void ModificationSwitch(Weapon weapon)
        {
            if (!isApplied)
            {
                ApplyModification(weapon);
                return;
            }
            DetachModification(weapon);
        }

        public void Fire()
        {
            _weapon.Fire();
        }
    }
}