using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    internal interface IAmmunition
    {
        Rigidbody BulletInstance { get; }
        float TimeToDestroy { get; }
    }
}