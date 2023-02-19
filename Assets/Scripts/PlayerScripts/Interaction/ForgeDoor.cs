using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class ForgeDoor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}
