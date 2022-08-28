using UnityEngine;

namespace RRRStudyProject
{
    public class MainCamera : IExecute
    {
        readonly GameObject _player;

        public MainCamera(GameObject player)
        {
            Object.FindObjectOfType<MainGame>().interactiveObjects.AddExecuteObj(this);
            _player = player;
        }

        public void Update()
        {
            Camera.main.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
        }
    }
}