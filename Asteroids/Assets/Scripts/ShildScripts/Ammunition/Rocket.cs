using UnityEngine;

namespace RRRStudyProject
{
    public class Rocket : Ammunition
    {
        private float startingEnergy = 10;
        public float StartingEnergy { get => startingEnergy; set => startingEnergy = value; }

        public Rocket(string ammoType) : base(ammoType)
        {
            this.ammoType = "Rocket";
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}