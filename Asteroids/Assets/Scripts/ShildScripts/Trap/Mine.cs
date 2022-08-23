using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Mine : Trap
    {
        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
                gameObject.SetActive(false);
                return;
            }
        }
    }
}

