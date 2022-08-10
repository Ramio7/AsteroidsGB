using System.IO;
using UnityEngine;

namespace RRRStudyProject.HW6
{
    public class JSONData<T>
    {
        public readonly string saveDirectoryPath = Path.Combine(Application.dataPath, "Data");
        public readonly string saveFileName = "JSONData.json";

        public void Save(T data)
        {
            if (!Directory.Exists(saveDirectoryPath)) Directory.CreateDirectory(saveDirectoryPath);
            string FileJSON = JsonUtility.ToJson(data);
            File.WriteAllText(Path.Combine(saveDirectoryPath, saveFileName), FileJSON);
        }

        public T Load(string saveFilePath)
        {
            if (!File.Exists(saveFilePath))
            {
                throw new System.Exception("Save file not found");
            }
            string jsonString = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}