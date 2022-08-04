using UnityEngine;

namespace RRRStudyProject
{
    public class Bullet : Ammunition
    {
        [SerializeField] private float _damage = 10;
        [SerializeField] private float _startingEnergy = 100;
        public Bullet(string ammoType) : base(ammoType) 
        {
        }

        public float Damage { get => _damage; set => _damage = value; }
        public float StartingEnergy { get => _startingEnergy; set => _startingEnergy = value; }

        public override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageInitializer victim))
            { 
                victim.DamageAgent.Hit(data.Damage);
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(false);
            Debug.Log("Didn't dealt any damage");
        }
    }
}