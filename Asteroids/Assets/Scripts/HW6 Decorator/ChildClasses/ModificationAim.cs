using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    internal sealed class ModificationAim : ModificationWeapon
    {
        readonly Camera _mainCamera = Camera.main;
        readonly IAim _aim;
        readonly Vector3 _aimPosition;

        public GameObject aim;

        public ModificationAim(IAim aim, Vector3 aimPosition)
        {
            _aim = aim;
            _aimPosition = aimPosition;
        }

        protected override Weapon AddModification(Weapon weapon)
        {
            aim = Object.Instantiate(_aim.AimInstance, _aimPosition, Quaternion.identity);
            _mainCamera.fieldOfView = _aim.AimFieldOfView;
            isApplied = true;
            return weapon;
        }

        protected override Weapon RemoveModification(Weapon weapon)
        {
            Object.Destroy(aim);
            _mainCamera.Reset();
            isApplied = false;
            return weapon;
        }
    }
}