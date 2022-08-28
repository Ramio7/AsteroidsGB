using UnityEngine;

namespace RRRStudyProject
{
    public class MovementAgent : IMove //проработать вращение объектов
    {
        private readonly GameObject _gameObjectToMove;
        private readonly CommandInput _commandInput;
        private readonly bool _isRigidbody;
        private float _movingSpeed;
        private float _rotationSpeed;

        public readonly Rigidbody2D rigidbody;

        public float MovingSpeed { get => _movingSpeed; set => _movingSpeed = value; }
        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

        public MovementAgent(GameObject objectToMove, IData objectData, CommandInput typeOfMovement)
        {
            _gameObjectToMove = objectToMove;
            _movingSpeed = objectData.Speed;
            _rotationSpeed = objectData.RotationSpeed;
            _commandInput = typeOfMovement;
            _isRigidbody = _gameObjectToMove.TryGetComponent(out Rigidbody2D tempRigidbody);
            rigidbody = tempRigidbody;
        }

        public float CalculateCollisionDamage()
        {
            if (rigidbody.velocity.magnitude == 0)
            {
                return rigidbody.mass * rigidbody.inertia;
            }
            return rigidbody.velocity.magnitude * rigidbody.inertia;
        }

        public void MoveViaStateMashine()
        {

        }

        public void Move()
        {
            if (_isRigidbody)
            {
                switch (_commandInput.inputType)
                {
                    case "UserInput":
                        {
                            rigidbody.AddRelativeForce(_movingSpeed * _commandInput.moveTowards.normalized);
                            break;
                        }
                    case "AIInput":
                        {
                            rigidbody.AddForce(_movingSpeed * Time.deltaTime * _commandInput.moveTowards);
                            break;
                        }
                    case "OneMoveInput":
                        {
                            rigidbody.AddForce(_movingSpeed * Time.deltaTime * _commandInput.moveTowards);
                            break;
                        }
                    default: throw new System.Exception("Invalid command input type");
                }
            }
            else
            {
                _gameObjectToMove.transform.position += _movingSpeed * Time.deltaTime * _gameObjectToMove.transform.TransformDirection(_commandInput.moveTowards.normalized);
            }
        }
    }
}