using ClickerTest.Data;
using System.IO;
using UnityEngine;

namespace ClickerTest.Tools.DataManipulation
{
    public class JsonSaveLoader 
    {
        private string SavePath => Application.persistentDataPath;
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

        private const string FileName = "ClickerPlayerData";
        private const string SaveFileExtension = "json";

        public void SaveData(PlayerProgress data)
        {
            string json = JsonUtility.ToJson(data);

            File.WriteAllText(FullPath, json);

            Debug.Log("Data has been saved");
        }

        public PlayerProgress TryLoadData()
        {
            if (!HasExistData())
            {
                Debug.Log("Save data has not been found");
                return null;
            }

            string loadedJson = File.ReadAllText(FullPath);
            
            Debug.Log("Data has been loaded");
            return JsonUtility.FromJson<PlayerProgress>(loadedJson);
        }

        public bool HasExistData()
        {
            return File.Exists(FullPath);
        }
    }
}