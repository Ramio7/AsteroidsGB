using UnityEngine;

namespace RRRStudyProject
{
    public class Torpedo : Ammunition
    {
        public Torpedo(string ammoType, float ammoCooldown, float startingEnergy, float damage) : base(ammoType, ammoCooldown, startingEnergy, damage)
        {
            this.ammoType = "Torpedo";
            this.ammoCooldown = ammoCooldown;
            this.startingEnergy = startingEnergy;
            this.damage = damage;
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}