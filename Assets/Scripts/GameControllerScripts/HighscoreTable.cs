using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameControllerScripts 
{
    public class HighscoreTable : MonoBehaviour 
    {
        private List<HighscoreEntry> _highscoreEntryList;
        private List<HighscoreEntry> _scoresFuren;
        private List<HighscoreEntry> _scoresStatue;
        private List<HighscoreEntry> _scoresPeacock;

        public List<TextMeshPro> scoreboardFuren;
        public List<TextMeshPro> scoreboardStatue;
        public List<TextMeshPro> scoreboardPeacock;
        
        private double finalTime;
        public string scoreboardScene;
        public int bossNumber;
        public TimerSaveHandler _timerSaveHandler;

        private void Awake() {
            //Debug.Log(_highscoreEntryList);
            //Debug.Log(_scoresFuren);
            //Debug.Log(_scoresStatue);
            //Debug.Log(_scoresPeacock);
            
            if (File.Exists(Application.dataPath + "/scoreboardFile.json") == false) {
                CreateFile();
            }
            
            if (File.ReadAllText(Application.dataPath + "/scoreboardFile.json") == "{}") {
                AddHighscoreEntry(1, 96.53, "DEV");
                AddHighscoreEntry(1, 120, "DEV");
                AddHighscoreEntry(1, 139.30, "DEV");

                AddHighscoreEntry(2, 180.8, "DEV");
                AddHighscoreEntry(2, 226.36, "DEV");
                AddHighscoreEntry(2, 54.54, "DEV");

                AddHighscoreEntry(3, 97.12, "DEV");
                AddHighscoreEntry(3, 15.47, "DEV");
                AddHighscoreEntry(3, 98.14, "DEV");
            }

            if (File.ReadAllText(Application.dataPath + "/SaveSpace.json") != "{}") {
                if (SceneManager.GetActiveScene().name == scoreboardScene) {
                    //finalTime = _timerSaveHandler.LoadTime();
                    AddHighscoreEntry(bossNumber, _timerSaveHandler.LoadTime(), "Player");
                    File.WriteAllText(Application.dataPath + "/SaveSpace.json", "{}");
                }
            }
            
            string jsonString = File.ReadAllText(Application.dataPath + "/scoreboardFile.json");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            /*  Sorts each boss list and displays them */
            ScoreSort(highscores.scoresFuren, scoreboardFuren);
            ScoreSort(highscores.scoresStatue, scoreboardStatue);
            ScoreSort(highscores.scoresPeacock, scoreboardPeacock);
        }
        
        private void AddHighscoreEntry(int level, double time, string name) {
            /*  Creates temp HighscoreEntry with new values */
            HighscoreEntry highscoreEntry = new HighscoreEntry {level = level, time = time, name = name};

            /*  Load data from saved Highscores file */
            string jsonString = File.ReadAllText(Application.dataPath + "/scoreboardFile.json");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            /*  Adds new entry to the correct list */
            highscores.highscoreEntryList.Add(highscoreEntry);
            if (level == 1) { highscores.scoresFuren.Add(highscoreEntry); }
            if (level == 2) { highscores.scoresStatue.Add(highscoreEntry); }
            if (level == 3) { highscores.scoresPeacock.Add(highscoreEntry); }

            /*  Saves the updated lists to the save file */
            string json = JsonUtility.ToJson(highscores);
            File.WriteAllText(Application.dataPath + "/scoreboardFile.json", json);
        }
        
        private void ScoreSort(List<HighscoreEntry> scores, List<TextMeshPro> boards) {
            /*  Runs through each time in the list and rearranges them in the correct order */
            for (int i = 0; i < scores.Count - 1; i++) {
                for (int j = i + 1; j < scores.Count; j++) {
                    if (scores[j].time < scores[i].time) {
                        HighscoreEntry tmp = scores[i];
                        scores[i] = scores[j];
                        scores[j] = tmp;
                    }
                }
            }

            /*  Sets each entry in the list to its corresponding text */
            int loopIndex = 0;
            foreach (HighscoreEntry highscoreEntry in scores) {
                TimeSpan time = TimeSpan.FromSeconds(highscoreEntry.time);
                boards[loopIndex].text = highscoreEntry.name + " - " + time.ToString("mm':'ss'.'ff");
                loopIndex++;
                if (loopIndex > 5) {
                    break;
                }
            }
        }

        /*  Represents all the necessary lists to be saved */
        private class Highscores {
            public List<HighscoreEntry> highscoreEntryList;
            public List<HighscoreEntry> scoresFuren;
            public List<HighscoreEntry> scoresStatue;
            public List<HighscoreEntry> scoresPeacock;
        }

        /*  Represents a single High score Entry */
        [System.Serializable]
        private class HighscoreEntry {
            public int level;
            public double time;
            public string name;
        }

        /*  Used to create a new file for saving the scores if one isn't available */
        private void CreateFile() {
            Highscores highscoresTemp = new Highscores {highscoreEntryList = _highscoreEntryList, scoresFuren = _scoresFuren, scoresStatue = _scoresStatue, scoresPeacock = _scoresPeacock};
            Debug.Log(highscoresTemp.highscoreEntryList);
            string json = JsonUtility.ToJson(highscoresTemp);
            Debug.Log(json);
            File.WriteAllText(Application.dataPath + "/scoreboardFile.json", json);
        }
    }
}

