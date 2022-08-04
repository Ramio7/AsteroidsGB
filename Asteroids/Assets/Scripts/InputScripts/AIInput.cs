using UnityEngine;
using Random = UnityEngine.Random;

namespace RRRStudyProject
{
    public class AIInput : CommandInput
    {
        public Vector2 _waypoint = new Vector2(0, 0);
        public float _speed;
        public float _rotationSpeed;

        public AIInput(string inputTypeName, GameObject controlledObject) : base(inputTypeName, controlledObject)
        {
            controlledObject.TryGetComponent(out Unit unit);
            _speed = unit.Data.Speed;
            _rotationSpeed = unit.Data.RotationSpeed;
        }

        private Vector2 GenerateWaypoint(Transform controlledObjectTransform, Vector2 currentWaypoint)
        {
            Vector2 _newWaypoint;
            GetPlayerPosition(out float xCoordinate, out float yCoordinate);
            if (Vector2.Distance(controlledObjectTransform.position, new Vector2(xCoordinate, yCoordinate)) >= 50f)
            {
                _newWaypoint.x = xCoordinate + Random.Range(-10f, 10f);
                _newWaypoint.y = yCoordinate + Random.Range(-10f, 10f);
                return _newWaypoint;
            }
            if (Vector2.Distance(currentWaypoint, controlledObjectTransform.position) <= 3f || Vector2.Distance(currentWaypoint, controlledObjectTransform.position) >= 30f)
            {
                _newWaypoint.x = currentWaypoint.x + Random.Range(-10f, 10f);
                _newWaypoint.y = currentWaypoint.y + Random.Range(-10f, 10f);
                return _newWaypoint;
            }
            return currentWaypoint;
        }

        private Vector2 ThrottleCalculator(Vector2 controlledObjectPosition, Vector2 currentWaypoint)
        {
            if (Vector2.Distance(controlledObjectPosition, currentWaypoint) >= 30) return currentWaypoint - controlledObjectPosition;
            if (Vector2.Distance(controlledObjectPosition, currentWaypoint) <= 10) return controlledObjectPosition - currentWaypoint;
            return Vector2.zero;
        }

        private bool OpenFire(Vector2 controlledObjectPosition)
        {
            GetPlayerPosition(out float xCoordinate, out float yCoordinate);
            if (Vector2.Distance(controlledObjectPosition, new Vector2(xCoordinate, yCoordinate)) < 30) return true;
            return false;
        }

        public override void Update()
        {
            _waypoint = GenerateWaypoint(controlledObject.transform, _waypoint);
            moveTowards = ThrottleCalculator(controlledObject.transform.position, _waypoint);
            rotationAngle = Vector2.SignedAngle(controlledObject.transform.position, moveTowards);
            isFiring = OpenFire(controlledObject.transform.position);
        }
    }
}
