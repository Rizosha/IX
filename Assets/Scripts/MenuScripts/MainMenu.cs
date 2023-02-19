using GameControllerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class MainMenu : MonoBehaviour
    {
        void Start() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
        }

        public void PlayGame()
        { SceneManager.LoadScene("MainScene"); }
        public void Next()
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); }
        public void Settings()
        { SceneManager.LoadScene("SettingMenu"); }
        public void Back()
        { SceneManager.LoadScene("MainMenu"); }
        public void Play()
        { SceneManager.LoadScene("Level_01"); }
        public void ResetProgress()
        {ProgressSaver.instance.SaveProgress(0);}
        public void QuitGame()
        { Application.Quit(); }
    }
}
