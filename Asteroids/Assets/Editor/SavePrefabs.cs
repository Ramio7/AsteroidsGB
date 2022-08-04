using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace RRRStudyProject
{
    [CustomEditor(typeof(GamePrefabs))]
    public class SaveBonuses : Editor  //Доделать оболочку для работы с префабами, когда появится время
    {
        [SerializeField] MonoScript _gamePrefabs;
                
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
            if (GUILayout.Button("Сохранить"))
            {
                
            }
        }
    }
}