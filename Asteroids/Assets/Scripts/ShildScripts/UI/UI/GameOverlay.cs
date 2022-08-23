using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class GameOverlay : UILayer, IExecute
    {
        public ScoreBar scoreCounter;
        public PlayerBar playerBar;

        public GameOverlay(GameObject layerObject, Player player) : base(layerObject)
        {
            uILayerName = "GameOverlay";
            Text[] _textObjectsInLayer = layerObject.GetComponentsInChildren<Text>();
            foreach (Text _textObject in _textObjectsInLayer)
            {
                if (_textObject.gameObject.name == "ScoreBar")
                {
                    scoreCounter = new ScoreBar(_textObject.gameObject);
                }
                if (_textObject.gameObject.name == "PlayerBar")
                {
                    playerBar = new PlayerBar(_textObject.gameObject, player);
                }
                if (scoreCounter != null && playerBar != null) return;
            }
            if (scoreCounter == null) throw new System.Exception("No ScoreBar object on layer");
            if (playerBar == null) throw new System.Exception("No PlayerBar object on layer");
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}