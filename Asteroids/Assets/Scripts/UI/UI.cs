using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    public class UI : IExecute
    {
        private readonly UserInput _userInput;

        public readonly Canvas UICanvas;

        public Dictionary<string, UILayer> UILayersList = new Dictionary<string, UILayer>();

        public UI(UserInput userInput, UILayer[] UILayers)
        {
            _userInput = userInput;

            for (int i = 0; i < UILayers.Length; i++)
            {
                UILayersList.Add(UILayers[i].UILayerName, UILayers[i]);
            }

            MainGame _mainGame = Object.FindObjectOfType<MainGame>();
            _mainGame.interactiveObjects.AddExecuteObj(this);
        }

        public void Update()
        {
            if (_userInput.pauseMenu) SetActiveUI("PauseMenu");
            if (_userInput.mainMenu) SetActiveUI("MainMenu");
            if (_userInput.inGame) SetActiveUI("GameOverlay");
        }

        public void SetActiveUI(string UILayerName)
        {
            if (!UILayersList.ContainsKey(UILayerName)) throw new System.Exception($"{UILayerName} is not created");
            ChooseLayerInDictionary(UILayersList[UILayerName]);
            if (!UILayersList[UILayerName].LayerObject.activeSelf) ActivateLayer(UILayersList[UILayerName]);
        }

        public void ActivateLayer(UILayer UILayer)
        {
            if (UILayer.LayerObject == null) throw new System.Exception($"{UILayer.UILayerName} object is not created");
            UILayer.LayerObject.SetActive(true);
        }

        public void DeactivateLayer(UILayer UILayer)
        {
            UILayer.LayerObject.SetActive(false);
        }

        private void ChooseLayerInDictionary(UILayer UILayer)
        {
            for (int i = 0; i < UILayersList.Count; i++)
            {
                UILayer[] _uILayers = new UILayer[UILayersList.Count];
                UILayersList.Values.CopyTo(_uILayers, 0);
                if (UILayer != _uILayers[i])
                {
                    DeactivateLayer(UILayersList[_uILayers[i].UILayerName]);
                }
            }
        }
    }
}