using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Trap : MonoBehaviour, IDamageInitializer, IGameObject
    {
        protected TrapData data;
        protected DamageAgent damageAgent;
        protected AmmunitionAgent trapAmmunition;

        public DamageAgent DamageAgent { get => damageAgent; set => damageAgent = value; }
        public IData Data { get => data; set => data = (TrapData)value; }
        public AmmunitionAgent AmmunitionAgent { get => trapAmmunition; set => trapAmmunition = value; }

        public abstract void OnTriggerEnter(Collider other);

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            {
                victim.DamageAgent.Hit(data.Damage);
                return;
            }
            Debug.Log("Didn't dealt any damage");
        }
    }
}