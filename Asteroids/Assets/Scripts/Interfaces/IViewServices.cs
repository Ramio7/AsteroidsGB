using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public interface IViewServices
    {
        T Instantiate<T>(GameObject prefab);

        void Destroy(GameObject value);
    }
}