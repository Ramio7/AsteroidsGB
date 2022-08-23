using UnityEngine;

namespace RRRStudyProject
{
    public class MainCamera : IExecute
    {
        GameObject _player;

        public MainCamera(ListExecuteObject interactiveObjects, GameObject player)
        {
            interactiveObjects.AddExecuteObj(this);
            _player = player;
        }

        public void Update()
        {
            Camera.main.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
        }
    }
}