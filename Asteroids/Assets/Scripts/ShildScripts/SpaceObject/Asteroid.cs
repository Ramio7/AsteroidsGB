using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Asteroid : SpaceObject //Доработать движение
    {
        public string className = "Asteroid";
        public float asteroidSpeed = 5;
        public float asteroidMaxHealth = 150;
        public float asteroidRotationSpeed = 0;
        private Vector3 asteroidDestination;

        public Vector3 AsteroidDestination { get => asteroidDestination; set => asteroidDestination = value; }

        private void Update()
        {
            movement.Move();
            Data.Damage = data.CalculateCollisionDamage(tempRigidbody);
        }
    }
}