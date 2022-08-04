using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Mine : Trap
    {
        public string className = "Mine";
        public float mineDamage;
        public float mineRadiusOfFire;
        public float mineMaxHealth;

        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
                gameObject.SetActive(false);
                return;
            }
            Debug.Log("Didn't dealt any damage");
        }
    }
}

