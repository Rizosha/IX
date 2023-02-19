using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameControllerScripts
{
    public class TimerController : MonoBehaviour
    {
        public static TimerController instance;
        public Text timeCounter;
        public TimeSpan timePlaying;
        private bool timerGoing;
        private float elapsedTime;
        private TimeSpan finalTime;
        private double newStart;

        public TimerSaveHandler _timerSaveHandler;

        private void Awake() { instance = this; }

        public void BeginTimer() {
            if (SceneManager.GetActiveScene().buildIndex != 2) {
                //elapsedTime = (float)TimerSaveHandler.instance.LoadTime();
            }
            else { elapsedTime = 0f; }
        
            timerGoing = true;
            StartCoroutine(UpdateTimer());
        }

        public void EndTimer() { timerGoing = false; _timerSaveHandler.SaveTime();}

        private IEnumerator UpdateTimer() {
            while (timerGoing) {
                elapsedTime += Time.deltaTime;
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
                timeCounter.text = timePlayingStr;

                yield return null;
            }
        }
    }
}
