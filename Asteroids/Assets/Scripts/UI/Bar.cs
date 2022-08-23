using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class Bar : IUIElement
    {
        protected RectTransform barRecttransfrom;
        protected GridLayoutGroup barGrid;
        protected GameObject barGameObject;

        public Bar(Vector2 barPosition, Vector2 barSize, Canvas parentCanvas)
        {
            barGameObject = new GameObject("Bar");

            barRecttransfrom = barGameObject.AddComponent<RectTransform>();
            barRecttransfrom.anchorMin.Set(0, 1);
            barRecttransfrom.anchorMax.Set(0, 1);
            barRecttransfrom.rect.Set(barPosition.x, barPosition.y, barSize.x, barSize.y);

            barGameObject.AddComponent<CanvasRenderer>();

            barGrid = barGameObject.AddComponent<GridLayoutGroup>();

            barGameObject.transform.SetParent(parentCanvas.transform);
        }

        public Bar(GameObject barGameObject)
        {
            this.barGameObject = barGameObject;
        }
    }
}