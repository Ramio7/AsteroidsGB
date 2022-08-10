using UnityEngine;

namespace RRRStudyProject
{
    public class Rocket : Ammunition
    {
        public Rocket(string ammoType, float ammoCooldown, float startingEnergy, float damage) : base(ammoType, ammoCooldown, startingEnergy, damage)
        {
            this.ammoType = "Rocket";
            this.ammoCooldown = ammoCooldown;
            this.startingEnergy = startingEnergy;
            this.damage = damage;
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
    }
}