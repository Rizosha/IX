using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class throneToArena : MonoBehaviour
    {
        /// <summary>
        /// Loads the Arena on Trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
