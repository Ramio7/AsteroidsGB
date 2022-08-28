using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class MessageBar : Bar, IExecute, ILogCreatedObject
    {
        private Text _messageText;

        public MessageBar(GameObject barGameObject) : base(barGameObject)
        {
            _messageText = barGameObject.GetComponent<Text>();
            Object.FindObjectOfType<MainGame>().interactiveObjects.AddExecuteObj(this);
        }

        public void LogCreatedObject(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IGameObject type))
                _messageText.text += $"{type.GetType().Name} created at coordinates {gameObject.transform.position}\n";
            else throw new System.Exception("No behavior script found");
        }

        public void ObjectDestroyed(string objectName)
        {
            _messageText.text += $"{objectName} has been destroyed\n";
        }

        public void Update()
        {
        }
    }
}