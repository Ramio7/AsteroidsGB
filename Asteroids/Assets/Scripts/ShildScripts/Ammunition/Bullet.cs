using UnityEngine;

namespace RRRStudyProject
{
    public class Bullet : Ammunition
    {
        private void OnEnable()
        {
            ammoType = "Bullet";
            ammoLifeTime = 5.0f;
            ammoCooldown = 0.5f;
            ammoAmountBonus = 50;
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim)) victim.DamageAgent.Hit(data.Damage);
            gameObject.SetActive(false);
        }
    }
}