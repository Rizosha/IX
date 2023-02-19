using UnityEngine;

namespace UI
{
    public class Exclamation : MonoBehaviour
    {
        public Transform cam;

        public float speed;
  
        public void Update()
        {
            /*float y = Mathf.PingPong(Time.time * speed, 1) * 6 - 3;
            var position = transform.position;
            position = new Vector3(position.x, position.y + y, position.z);
            transform.position = position;*/
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
         
        }
    }
}
