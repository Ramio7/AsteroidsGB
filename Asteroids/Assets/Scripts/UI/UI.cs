using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class UI : IExecute
    {
        readonly MainGame game = Object.FindObjectOfType<MainGame>();

        private readonly DamageAgent _playerDamageAgent;
        private TextMeshPro _HPTextMesh;
        private Scrollbar _HPScrollBar;

        public UI(GameObject playerObject)
        {
            Player _player = playerObject.GetComponent<Player>();
            _playerDamageAgent = _player.DamageAgent;

            GameObject _HPTextMeshObject = GameObject.Find("HealthBarText");
            GameObject _HPScrollBarObject = GameObject.Find("HealthBarVisual");

            _HPTextMesh = _HPTextMeshObject.AddComponent<TextMeshPro>();
            _HPScrollBar = _HPScrollBarObject.GetComponent<Scrollbar>();

            game.interactiveObjects.AddExecuteObj(this);
        }

        public void Update()
        {
            _HPScrollBar.size = _playerDamageAgent.currentHealth / _playerDamageAgent.maxHealth;
            _HPTextMesh.SetText($"Health: {_playerDamageAgent.currentHealth}");
        }
    }
}