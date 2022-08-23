using UnityEngine;

namespace RRRStudyProject
{
    public class PointInTime
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public float AngularVelocity;
        public PointInTime(Vector3 position, Quaternion rotation, Vector3
        velocity, float angularVelocity)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
        }
    }
}