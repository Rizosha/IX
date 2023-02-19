using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class throneToPrison : MonoBehaviour
    {
        /// <summary>
        /// Loads the prison scene on trigger
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
