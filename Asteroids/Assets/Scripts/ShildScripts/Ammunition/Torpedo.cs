using UnityEngine;

namespace RRRStudyProject
{
    public class Torpedo : Ammunition
    {
        private void OnEnable()
        {
            ammoLifeTime = 20.0f;
            ammoType = "Torpedo";
            ammoCooldown = 4f;
            ammoAmountBonus = 3;
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