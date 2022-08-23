using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace RRRStudyProject
{
    public class ScoreBar : Bar, IExecute
    {
        private readonly Text _scoreText;
        private ulong _currentScore = 0;

        public ScoreBar(Vector2 barPosition, Vector2 barSize, Canvas parentCanvas) : base(barPosition, barSize, parentCanvas)
        {
            barGameObject.name = "ScoreBar";
            barRecttransfrom.anchorMin = new Vector2(0.5f, 1f);
            barRecttransfrom.anchorMax = new Vector2(0.5f, 1f);
            barRecttransfrom.position = barPosition;
            _scoreText = barGameObject.AddComponent<Text>();
        }

        public ScoreBar(GameObject barGameObject) : base(barGameObject)
        {
            _scoreText = barGameObject.GetComponent<Text>();
            _scoreText.text = "0";
            Object.FindObjectOfType<MainGame>().interactiveObjects.AddExecuteObj(this);
        }

        public void AddScore(int score)
        {
            if (!ulong.TryParse(score.ToString(), out ulong _score)) throw new ArgumentOutOfRangeException("Invalid score input");
            _currentScore += _score;
            _scoreText.text = TextInterpreter.Interpret(_currentScore.ToString());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) AddScore(100);
            if (Input.GetKeyDown(KeyCode.K)) AddScore(1000);
            if (Input.GetKeyDown(KeyCode.M)) AddScore(1000000);
            if (Input.GetKeyDown(KeyCode.B)) AddScore(1000000000);
        }
    }
}