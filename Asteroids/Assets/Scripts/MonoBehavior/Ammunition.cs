using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Ammunition : MonoBehaviour
    {
        protected string ammoType;

        protected int ammoAmount;

        protected float ammoLifeTime;

        protected AmmunitionData data;

        public float ammoCooldown;
        public float damage;
        public float startingEnergy;

        protected Ammunition(string ammoType, float ammoCooldown, float startingEnergy, float damage)
        {
            this.ammoType = ammoType;
            this.ammoCooldown = ammoCooldown;
            this.startingEnergy = startingEnergy;
            this.damage = damage;
            data = new AmmunitionData(ammoType);
        }

        public IData Data { get => data; set => data = (AmmunitionData)value; }

        public abstract void OnCollisionEnter2D(Collision2D collision);

        public void Start() //внести отправку в пул после деактивации пули
        {
            
        }
    }
}