using EnemyScripts.Lion;
using MenuScripts;
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameControllerScripts
{
    public class GameController : MonoBehaviour
    {
        /*  All the necessary variables to control the scene */
        private Transform _player;
        private Animator _playerAnimator;
        private PlayerController _playerController;
        private LionStateManager _lionController;
        public GameObject canvas;
        private PauseMenu _pauseMenu;
        public GameObject deathMenuUI;
        //private GameObject _potion;
        private TimerController _timerController;
        
        private float timeBeforeTransition = 5f;
        private float timeSinceDeath;
        
        public GateScript prisonGate, armoryGate, throneGate;
        public GameObject arenaSceneSwitch;
    
        public float potionTime;
        public float potionRespawnTime;

        /*  Sets all necessary components from the scene */
        private void Awake() {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerAnimator = _player.GetComponent<Animator>();
            _playerController = _player.GetComponent<PlayerController>();
            _lionController = GameObject.Find("LionController").GetComponent<LionStateManager>();
            canvas = GameObject.Find("Canvas");
            _pauseMenu = canvas.GetComponent<PauseMenu>();
            deathMenuUI = GameObject.Find("DeathScreen");
            //_potion = GameObject.Find("Potion");
            _timerController = GetComponent<TimerController>();
            
            //_canvas.SetActive(true);
        }

        /*  Starts the timer within the scene
            Need to be changed so only runs in the right scenes */
        void Start() { BeginTimer(); }

        void Update() {
            /*  Activates all scene changes when player interacts with the gates */
            if (prisonGate.colliding) { SceneManager.LoadScene("PrisonScene"); }
            if (armoryGate.colliding) { SceneManager.LoadScene("ArmouryScene"); }
            if (throneGate.colliding) { SceneManager.LoadScene("ScoreRoomScene"); }

            /*  activates the exit when the lion has died
                should be moved away from universal game controller to just main scene controller */
            if (_lionController.dead) { arenaSceneSwitch.SetActive(true); }

            /*  tracks time since death so the death screen can activate */
            if (_playerAnimator.GetBool("isDead")) { timeSinceDeath += Time.deltaTime; }
        
            /*  Activates the death UI and pauses the game */
            if (timeSinceDeath >= timeBeforeTransition) {
                deathMenuUI.SetActive(true);
                _pauseMenu.GameIsPaused = true;
            }

            /*  Tracks teh time until the potion can spawn */
            if (_playerController.potionOn) { potionTime += Time.deltaTime; }

            /*  Activates the potion in the scene */
            if (potionTime >= potionRespawnTime) {
                // potionCanSpawn = true;
                _playerController.potion.SetActive(true);
                _playerController.potionOn = false;
                potionTime = 0f;
            }
        }
    
        /*  Calls the start timer function from the timer controller */
        private void BeginTimer() { _timerController.BeginTimer();}
        
        /*  Calls the End timer function from the timer controller */
        public void EndTimer() { _timerController.EndTimer(); }

    }
}
