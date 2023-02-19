using PlayerScripts.Player;
using UnityEngine;
//using PlayerScripts.PlayerStateMachine;

namespace PlayerScripts.Interaction
{
    public class Potion : MonoBehaviour
    {
        /// <summary>
        /// This script is for rotating the potion and giving player health
        /// </summary>
        public float gizmo;
        private bool inRange;
        public LayerMask player;
        [SerializeField] public PlayerController playerH;
        public float speed = 10f;

        private void Start()
        {
            playerH = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

   
        void Update()
        {
            // rotates the potion at a set speed. 
            transform.Rotate(Vector3.up + Vector3.left * Time.deltaTime * speed);
        
            //check sphere to see if the player is nearby, 
            inRange = Physics.CheckSphere(transform.position, gizmo, player);
        
            //if in range is met and E is pressed, adds 50 health and destroys
            if (inRange && playerH.interactPressed)
            {
                playerH.GiveHealth(50);
                //Destroy(gameObject);
            }
        }
    
        // gizmo
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, gizmo);
        }
    }
}
