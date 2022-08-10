using UnityEngine;

namespace RRRStudyProject
{
    public class Bullet : Ammunition
    {
        public Bullet(string ammoType, float ammoCooldown, float startingEnergy, float damage) : base(ammoType, ammoCooldown, startingEnergy, damage)
        {
            this.damage = damage;
            this.startingEnergy = startingEnergy;
            this.ammoCooldown = ammoCooldown;
            this.ammoType = "Bullet";
        }

        private void OnEnable()
        {
            TryGetComponent(out Rigidbody2D rigidbody2D);
            rigidbody2D.AddRelativeForce(Vector2.up * startingEnergy);
        }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            { 
                victim.DamageAgent.Hit(data.Damage);
            }
            gameObject.SetActive(false);
        }
    }
}