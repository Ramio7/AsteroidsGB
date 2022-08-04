using UnityEngine;

namespace RRRStudyProject
{
    public abstract class Ammunition : MonoBehaviour, IHaveCooldowns
    {
        protected string ammoType;
        protected int ammoAmount;
        protected float ammoCreateTime = Time.time;
        protected float ammoDestructionTime;

        public bool isActive = true;

        public AmmunitionData data;
        public string AmmoType { get => ammoType; set => ammoType = value; }

        protected Ammunition(string ammoType)
        {
            this.ammoType = ammoType;
        }

        public IData Data { get => data; set => data = (AmmunitionData)value; }

        public abstract void OnCollisionEnter2D(Collision2D collision);

        public void Start()
        {
            gameObject.TryGetComponent(out Rigidbody2D ammoRigidbody);
            ammoRigidbody.AddRelativeForce(Data.Speed * Vector2.up);
        }

        public void Update() //внести отправку в пул после деактивации пули
        {
            
        }

        public bool Cooldown(float cooldownStart, float cooldownAmount, out float cooldownStarted)
        {
            cooldownStarted = 0;
            if (Time.time > cooldownStart + cooldownAmount) return true;
            return false;
        }
    }
}