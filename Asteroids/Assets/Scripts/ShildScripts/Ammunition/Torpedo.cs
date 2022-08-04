using UnityEngine;

namespace RRRStudyProject
{
    public class Torpedo : Ammunition
    {
        private float startingEnergy = 10;
        public float StartingEnergy { get => startingEnergy; set => startingEnergy = value; }

        public Torpedo(string ammoType) : base(ammoType)
        {
            this.ammoType = "Torpedo";
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}