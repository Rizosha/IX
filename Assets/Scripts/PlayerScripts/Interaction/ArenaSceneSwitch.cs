using GameControllerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class ArenaSceneSwitch : MonoBehaviour
    {
        public TimerController _timerController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timerController.EndTimer();
                SceneManager.LoadScene(4);
            }
        }
    }
}
