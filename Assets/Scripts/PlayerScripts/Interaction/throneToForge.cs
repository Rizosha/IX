using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class throneToForge : MonoBehaviour
    {
        /// <summary>
        /// Loads the Throne scene on trigger
        /// </summary>
        /// <param name="other"></param>
   
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
