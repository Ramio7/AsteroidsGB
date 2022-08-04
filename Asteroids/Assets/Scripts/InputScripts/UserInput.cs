using UnityEngine;

namespace RRRStudyProject
{
    public sealed class UserInput : CommandInput
    {
        readonly Camera _camera = Camera.main;

        public UserInput(string inputTypeName, GameObject controlledObject) : base(inputTypeName, controlledObject)
        {
        }

        public override void Update()
        {
            moveTowards = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            lookAt = MouseWorldPosition(_camera);
            if (Input.GetMouseButton(0)) isFiring = true;
        }

        private Vector3 MouseWorldPosition(Camera screenCamera)
        {
            Vector3 _tempMouseCoordinates = screenCamera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(_tempMouseCoordinates.x, _tempMouseCoordinates.y, _tempMouseCoordinates.z);
        }
    }
}