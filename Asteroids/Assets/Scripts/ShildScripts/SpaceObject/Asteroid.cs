using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Asteroid : SpaceObject //Доработать движение
    {
        public override void OnEnable()
        {
            className = "Asteroid";
            commandInput = new OneMoveInput(gameObject, GetPlayerData.GenerateWaypointNearPlayer());
        }

        private void Update()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
        }
    }
}