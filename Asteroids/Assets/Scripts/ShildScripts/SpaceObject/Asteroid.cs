using UnityEngine;

namespace RRRStudyProject
{
    public sealed class Asteroid : SpaceObject //���������� ��������
    {
        public Asteroid()
        {
            className = "Asteroid";
        }

        public override void OnEnable()
        {
            commandInput = new OneMoveInput(gameObject, GetPlayerData.GenerateWaypointNearPlayer());
        }

        private void Update()
        {
            movement.Move();
            Data.Damage = movement.CalculateCollisionDamage();
        }
    }
}