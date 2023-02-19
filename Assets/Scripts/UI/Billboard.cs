using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        public Transform cam;
    
        void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        
        }
    }
}
