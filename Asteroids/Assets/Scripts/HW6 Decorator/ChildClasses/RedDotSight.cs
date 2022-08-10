using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    public class RedDotSight : IAim
    {
        public float AimFieldOfView { get; }
        public Transform AimPosition { get; }
        public GameObject AimInstance { get; }

        public RedDotSight(float aimFieldOfView, Transform aimPosition, GameObject aimInstance)
        {
            AimFieldOfView = aimFieldOfView;
            AimPosition = aimPosition;
            AimInstance = aimInstance;
        }
    }
}