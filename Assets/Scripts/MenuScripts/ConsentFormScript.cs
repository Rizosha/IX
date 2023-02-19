using UnityEngine;

namespace MenuScripts
{
    public class ConsentFormScript : MonoBehaviour
    {
        public void Confirm() {
            gameObject.SetActive(false);
        }
    }
}
