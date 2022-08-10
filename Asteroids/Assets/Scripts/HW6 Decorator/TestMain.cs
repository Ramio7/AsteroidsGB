
using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    internal sealed class TestMain : MonoBehaviour
    {
        IFire _fire;

        [Header("Start Gun")]
        [SerializeField] Rigidbody _bullet;
        [SerializeField] Transform _barrelPosition;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] AudioClip _audioClip;

        [Header("Muffler Gun")]
        [SerializeField] AudioClip _audioClipMuffler;
        [SerializeField] float _volumeFireOnMuffler;
        [SerializeField] Transform _barrelPositionMuffler;
        [SerializeField] Transform _mufflerPosition;
        [SerializeField] GameObject _muffler;

        [Header("Gun with aim")]
        [SerializeField] float _aimFieldOfView;
        [SerializeField] Transform _aimPosition;
        [SerializeField] GameObject _redDotSight;

        Weapon weapon;
        Muffler muffler;
        IAim aim;

        ModificationMuffler modificationWeaponMuffler;
        ModificationWeapon modificationWeaponAim;

        private void Start()
        {
            IAmmunition ammunition = new Bullet(_bullet, 3.0f);

            weapon = new Weapon(ammunition, _barrelPosition, 999.0f, _audioSource, _audioClip);
            muffler = new Muffler(_audioClipMuffler, _volumeFireOnMuffler, _barrelPositionMuffler, _muffler);
            aim = new RedDotSight(_aimFieldOfView, _aimPosition, _redDotSight);

            modificationWeaponMuffler = new ModificationMuffler(_audioSource, muffler, _mufflerPosition.position);
            modificationWeaponAim = new ModificationAim(aim, aim.AimPosition.position);

            _fire = weapon;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) _fire.Fire();
            if (Input.GetMouseButtonDown(1))
            {
                modificationWeaponMuffler.ModificationSwitch(weapon);
                if (!modificationWeaponMuffler.isApplied)
                {
                    _fire = modificationWeaponMuffler;
                    return;
                }
                weapon.SetBarrelPosition(_barrelPosition);
                _fire = weapon;
            }
            if (Input.GetMouseButtonDown(2)) modificationWeaponAim.ModificationSwitch(weapon);
        }
    }
}