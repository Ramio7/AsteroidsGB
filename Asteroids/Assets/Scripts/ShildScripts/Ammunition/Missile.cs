using UnityEngine;

namespace RRRStudyProject
{
    public class Missile : Ammunition
    {
        private float startingEnergy = 10;
        public float StartingEnergy { get => startingEnergy; set => startingEnergy = value; }

        public Missile(string ammoType) : base(ammoType)
        {
            this.ammoType = "Missile";
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}