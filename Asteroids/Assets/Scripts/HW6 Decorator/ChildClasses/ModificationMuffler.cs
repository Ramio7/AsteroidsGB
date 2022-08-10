using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    internal sealed class ModificationMuffler : ModificationWeapon
    {
        readonly AudioSource _audioSource;
        readonly IMuffler _muffler;
        readonly Vector3 _mufflerPosition;

        public GameObject muffler;

        public ModificationMuffler(AudioSource audioSource, IMuffler muffler, Vector3 mufflerPosition)
        {
            _audioSource = audioSource;
            _muffler = muffler;
            _mufflerPosition = mufflerPosition;
        }

        protected override Weapon AddModification(Weapon weapon)
        {
            muffler = Object.Instantiate(_muffler.MufflerInstance, _mufflerPosition, Quaternion.AngleAxis(90, Vector3.right));
            _audioSource.volume = _muffler.VolumeFireOnMuffler;
            weapon.SetAudioClip(_muffler.AudioClipMuffler);
            weapon.SetBarrelPosition(_muffler.BarrelPositionMuffler);
            isApplied = true;
            return weapon;
        }

        protected override Weapon RemoveModification(Weapon weapon)
        {
            Object.Destroy(muffler);
            _audioSource.volume /= _muffler.VolumeFireOnMuffler;
            isApplied = false;
            return weapon;
        }
    }
}