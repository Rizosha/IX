using UnityEngine;

namespace PlayerScripts.Unused
{
    public class Follow : MonoBehaviour
    {
        public GameObject obj;
    

        // Update is called once per frame
        void Update()
        {
            transform.position = obj.transform.position;
        }
    }
}
