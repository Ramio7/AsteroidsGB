using System;
using UnityEngine;

namespace RRRStudyProject
{
    [Serializable]
    public sealed class UserInput : CommandInput
    {
        readonly Camera _camera = Camera.main;

        #region KeyCommands

        [SerializeField] KeyCommand moveForward = new KeyCommand(KeyCode.W);
        [SerializeField] KeyCommand moveBackward = new KeyCommand(KeyCode.S);
        [SerializeField] KeyCommand moveLeft = new KeyCommand(KeyCode.A);
        [SerializeField] KeyCommand moveRight = new KeyCommand(KeyCode.D);
        [SerializeField] KeyCommand lockWeapons = new KeyCommand(KeyCode.L);
        [SerializeField] KeyCommand pauseGame = new KeyCommand(KeyCode.P);
        [SerializeField] KeyCommand resumeGame = new KeyCommand(KeyCode.R);
        [SerializeField] KeyCommand speedBoost = new KeyCommand(KeyCode.LeftShift);

        #endregion

        public UserInput(GameObject controlledObject) : base(controlledObject)
        {
            inputType = "UserInput";
        }

        public override void Update()
        {
            if (inGame)
            {
                moveTowards = GetMovingDirection();
                lookAt = MouseWorldPosition(_camera);
                if (Input.GetMouseButton(0)) isFiring = true;
                if (lockWeapons.KeyDown) weaponLock = !weaponLock;

                if (pauseGame.KeyDown)
                {
                    Time.timeScale = 0.0f;
                    pauseMenu = true;
                    inGame = false;
                }

                if (speedBoost.Key)
                {

                }
            }
            if (pauseMenu)
            {
                if (resumeGame.KeyDown)
                {
                    Time.timeScale = 1.0f;
                    pauseMenu = false;
                    inGame = true;
                }
            }
            if (controlledObject == null)
            {
                controlledObject = UnityEngine.Object.FindObjectOfType<Player>().gameObject;
            }
        }

        private Vector3 MouseWorldPosition(Camera screenCamera)
        {
            Vector3 _tempMouseCoordinates = screenCamera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(_tempMouseCoordinates.x, _tempMouseCoordinates.y, _tempMouseCoordinates.z);
        }

        private Vector2 GetMovingDirection()
        {
            float xCoordinate = 0;
            float yCoordinate = 0;
            if (moveForward.Key) yCoordinate = 1;
            if (moveBackward.Key) yCoordinate = -1;
            if (moveLeft.Key) xCoordinate = -1;
            if (moveRight.Key) xCoordinate = 1;
            return new Vector2(xCoordinate, yCoordinate).normalized;
        }
    }
}