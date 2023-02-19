using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameControllerScripts 
{
    public class GateScript : MonoBehaviour 
    {
        public bool colliding;
        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                colliding = true;
            }
        }
    }
}
