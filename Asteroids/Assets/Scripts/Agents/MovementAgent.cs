using UnityEngine;

namespace RRRStudyProject
{
    public class MovementAgent : IMove //проработать вращение объектов
    {
        private readonly GameObject _gameObjectToMove;
        private readonly CommandInput _commandInput;
        private readonly bool _isRigidbody;
        private readonly Rigidbody2D _rigidbody;
        private float _movingSpeed;
        private float _rotationSpeed;

        public float MovingSpeed { get => _movingSpeed; set => _movingSpeed = value; }
        public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

        public MovementAgent(GameObject objectToMove, IData objectData, CommandInput typeOfMovement)
        {
            _gameObjectToMove = objectToMove;
            _movingSpeed = objectData.Speed;
            _rotationSpeed = objectData.RotationSpeed;
            _commandInput = typeOfMovement;
            _isRigidbody = _gameObjectToMove.TryGetComponent(out Rigidbody2D rigidbody);
            _rigidbody = rigidbody;
        }

        public void Move()
        {
            if (_isRigidbody)
            {
                switch (_commandInput.InputTypeName)
                {
                    case "UserInput":
                        {
                            _rigidbody.AddRelativeForce(_movingSpeed * _commandInput.moveTowards.normalized);
                            break;
                        }
                    case "AIInput":
                        {
                            _rigidbody.AddForce(_movingSpeed * Time.deltaTime * _commandInput.moveTowards);
                            break;
                        }
                    case "OneMoveInput":
                        {
                            _rigidbody.AddForce(_movingSpeed * Time.deltaTime * _commandInput.moveTowards);
                            break;
                        }
                }
            }
            else
            {
                _gameObjectToMove.transform.position += _movingSpeed * Time.deltaTime * _gameObjectToMove.transform.TransformDirection(_commandInput.moveTowards.normalized);
            }
        }
    }
}