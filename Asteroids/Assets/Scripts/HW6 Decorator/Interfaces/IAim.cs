using UnityEngine;

namespace RRRStudyProject.HW6.Decorator
{
    internal interface IAim
    {
        float AimFieldOfView { get; }
        Transform AimPosition { get; }
        GameObject AimInstance { get; }
    }
}