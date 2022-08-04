using System;
using UnityEngine;

namespace RRRStudyProject
{
    [Serializable]
    public struct SVector3
    {
        public float X;
        public float Y;
        public float Z;

        public SVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator SVector3(Vector3 value)
        {
            return new SVector3(value.x, value.y, value.z);
        }

        public static implicit operator Vector3(SVector3 value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
    }

    [Serializable]
    public struct SQuaternion
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public SQuaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static implicit operator SQuaternion(Quaternion value)
        {
            return new SQuaternion(value.x, value.y, value.z, value.w);
        }

        public static implicit operator Quaternion(SQuaternion value)
        {
            return new Quaternion(value.X, value.Y, value.Z, value.W);
        }
    }
}