using UnityEngine;

namespace RRRStudyProject
{
    public class Rocket : Ammunition
    {
        private void OnEnable()
        {
            ammoLifeTime = 7.0f;
            ammoType = "Rocket";
            ammoCooldown = 1f;
            ammoAmountBonus = 25;
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