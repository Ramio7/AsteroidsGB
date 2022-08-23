using UnityEngine;

namespace RRRStudyProject
{
    public class Missile : Ammunition
    {
        private void OnEnable()
        {
            ammoLifeTime = 15.0f;
            ammoType = "Missile";
            ammoCooldown = 1.5f;
            ammoAmountBonus = 10;
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
            }
            gameObject.SetActive(false);
        }
    }
}