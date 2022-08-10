using System;
using UnityEngine;

namespace RRRStudyProject.HW6
{
    [Serializable]
    public class Unit : ITestUnitData
    {
        [SerializeField] protected string type = "Unit";
        [SerializeField] protected int health = 1;

        public string Type { get => type; set => type = value; }
        public int Health { get => health; set => health = value; }
    }

    [Serializable]
    public class UnitRoot
    {
        public Unit[] unitArray;
    }
}