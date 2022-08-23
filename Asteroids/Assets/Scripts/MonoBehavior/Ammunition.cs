using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Ammunition : MonoBehaviour
    {
        public string ammoType;

        protected float ammoLifeTime; //inject all class fields in Data
        protected float ammoDestroyTime;

        public AmmunitionData data;

        public float ammoCooldown;
        public int ammoAmountBonus; //Add ingame bonuses for ammo amount in future

        public IData Data { get => data; set => data = (AmmunitionData)value; }

        public abstract void OnTriggerEnter2D(Collider2D collision);

        public void Start()
        {
            ammoDestroyTime = Time.time + ammoLifeTime;
        }

        public void Update()
        {
            if (Time.time > ammoDestroyTime) gameObject.SetActive(false);
        }
    }
}