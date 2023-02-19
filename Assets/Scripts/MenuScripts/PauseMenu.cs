using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using PlayerScripts.PlayerStateMachine;

namespace MenuScripts
{
    public class PauseMenu : MonoBehaviour
    {
        public bool GameIsPaused;
        public GameObject pauseMenuUI;
    
        public PlayerController player;
        private float maxTime = 1f;
        private float timer = 0.0f;
    
        [SerializeField]
        public InputActionReference menuOpenControl;
        public InputActionReference previousSceneControl;
        public InputActionReference nextSceneControl;
    
        private void OnEnable() {
            menuOpenControl.action.Enable();
            previousSceneControl.action.Enable();
            nextSceneControl.action.Enable();
        }

        private void OnDisable() {
            menuOpenControl.action.Disable();
            previousSceneControl.action.Disable();
            nextSceneControl.action.Disable();
        }

        private void Awake() { player = GameObject.Find("Player").GetComponent<PlayerController>(); }

        private void Start() { Resume(); }

        void Update() {
            timer -= Time.deltaTime;
        
            //To stop the player excepting commands when game is paused
            if (menuOpenControl.action.IsPressed() && timer < 0) {
                if (GameIsPaused) { Resume(); 
                    //player.OnEnable();
                }
                else { Pause(); 
                    //player.OnDisable();
                }
                timer = maxTime;
            }
            if (GameIsPaused) { Cursor.visible = true; Cursor.lockState = CursorLockMode.None; }
            else { Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; }

            //if (previousSceneControl.action.IsPressed()) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }
            //if (nextSceneControl.action.IsPressed()) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        }

        public void Resume() {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        void Pause() {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void LoadMenu() { SceneManager.LoadScene("MainMenu"); }
        public void Quit() { Application.Quit(); }
        public void Restart() { SceneManager.LoadScene("MainScene"); }
        public void Restart2() { SceneManager.LoadScene("TaylorTest"); }
        public void Settings() { SceneManager.LoadScene("SettingsMenu"); }
    }
}
