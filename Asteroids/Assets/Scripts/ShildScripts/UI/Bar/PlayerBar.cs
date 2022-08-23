using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class PlayerBar : Bar, IExecute
    {
        DamageAgent _player;

        private readonly Scrollbar _healthLine;
        private readonly Text _healthText;
        private readonly Text _playerName;
        private Image _playerIcon;

        public PlayerBar(GameObject barGameObject, Player player) : base(barGameObject)
        {
            _player = player.DamageAgent;

            _playerName = barGameObject.GetComponent<Text>();
            _playerName.text = player.playerName;

            _playerIcon = barGameObject.GetComponentInChildren<Image>();

            _healthLine = barGameObject.GetComponentInChildren<Scrollbar>();

            _healthText = _healthLine.gameObject.GetComponentInChildren<Text>();

            Object.FindObjectOfType<MainGame>().interactiveObjects.AddExecuteObj(this);
        }

        public void Update()
        {
            if (_player.currentHealth <= 0) _healthLine.targetGraphic.color = new Color(0, 0, 0, 0);
            _healthLine.size = _player.currentHealth / _player.maxHealth;
            _healthText.text = $"{_player.currentHealth} / {_player.maxHealth}";
        }
    }
}