using UnityEngine;
using UnityEngine.UI;

namespace RRRStudyProject
{
    public class MessageBar : Bar
    {
        private Text _messageText;

        public MessageBar(GameObject barGameObject) : base(barGameObject)
        {
            _messageText = barGameObject.GetComponent<Text>();
        }
    }
}