using System.IO;
using UnityEngine;

namespace GameControllerScripts
{
    public class ProgressSaver : MonoBehaviour{

        public static ProgressSaver instance;
        private void Awake() { instance = this; }

        private SaveData saveData = new SaveData();
    
        public void SaveProgress(int scene) {
            saveData.progress = scene;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.dataPath + "/saveSpace.json", json);
        }

        public int LoadProgress() {
            string json = File.ReadAllText(Application.dataPath + "/saveSpace.json");
            SaveData loadedProgress = JsonUtility.FromJson<SaveData>(json);
            int loadedTime = loadedProgress.progress;

            return loadedTime;
        }
    
        private class SaveData {
            public int progress;
        }
    }
}
