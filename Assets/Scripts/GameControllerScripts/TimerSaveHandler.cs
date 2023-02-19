using System.IO;
using UnityEngine;

namespace GameControllerScripts
{
    public class TimerSaveHandler : MonoBehaviour
    {
        public static TimerSaveHandler instance;
        private void Awake() { instance = this; }

        private SaveData saveData = new SaveData();
        public TimerController _timerController;

        public void SaveTime() {
            saveData.time = _timerController.timePlaying.TotalSeconds;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.dataPath + "/saveSpace.json", json);
        }

        public double LoadTime() {
            string json = File.ReadAllText(Application.dataPath + "/saveSpace.json");
            SaveData loadedTimerData = JsonUtility.FromJson<SaveData>(json);
            double loadedTime = loadedTimerData.time;

            return loadedTime;
        }
    
        public void SaveName(string nameInput) {
            saveData.name = nameInput;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.dataPath + "/saveSpace.json", json);
        }
    
        public string LoadName() {
            string json = File.ReadAllText(Application.dataPath + "/saveSpace.json");
            SaveData loadedTimerData = JsonUtility.FromJson<SaveData>(json);
            string loadedName = loadedTimerData.name;

            return loadedName;
        }

        private class SaveData {
            public string name;
            public double time;
        
        }
    }
}
