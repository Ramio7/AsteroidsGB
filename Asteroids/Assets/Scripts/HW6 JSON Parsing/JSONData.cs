using System.IO;
using UnityEngine;

namespace RRRStudyProject.HW6
{
    public class JSONData
    {
        public void Save(TestUnit unit)
        {
            string FileJSON = JsonUtility.ToJson(unit);
            File.AppendAllText(Path.Combine(Application.dataPath, $"JSONUnitData.json"), FileJSON);
        }

        public string Load(string saveDataPath)
        {
            if (!File.Exists(saveDataPath))
            {
                throw new System.Exception("Save file not found");
            }
            return File.ReadAllText(saveDataPath);
        }

        public TestUnit[] ParseData(string JSONstring)
        {
            string _jsonPattern = "(unit\\.?)"; //записать правильный паттерн для разделения на строки
            string[] _tempData = JSONstring.Split('\u002C');
            return null;
        }
    }
}