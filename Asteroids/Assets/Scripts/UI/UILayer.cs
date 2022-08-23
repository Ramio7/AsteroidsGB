using UnityEngine;

namespace RRRStudyProject
{
    public abstract class UILayer : IUILayer
    {
        protected string uILayerName;
        readonly GameObject _layerObject;

        public UILayer(GameObject layerObject)
        {
            _layerObject = layerObject;
        }

        public string UILayerName => uILayerName;

        public GameObject LayerObject => _layerObject;
    }
}