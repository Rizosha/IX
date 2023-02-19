using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts.Oldscript
{
    public class EnemyController : MonoBehaviour
    {
        /* Enemy Controller To Do:
     * - Always following player
     * - Stops within range of player
     * - Does action/attack
     * - Turns to react to player
     * 
     */

        //TODO example
    
        public Transform player;
        private NavMeshAgent navMeshAgent;
        public Animator animator;
        private void Awake() { animator = GetComponent<Animator>(); }
    
        void Start() {
            player = GameObject.Find("PlayerController").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
            Physics.IgnoreLayerCollision(10, 11);
        }

        void Update() {
            float currentDis = Vector3.Distance(transform.position, player.position);
            Debug.DrawLine(transform.position, player.position, Color.blue);
            navMeshAgent.destination = player.position;
            animator.SetBool("isWalking", true);
        
            if (IsFront() && Vector3.Distance(transform.position, player.transform.position) < 2f) {
                animator.SetBool("isWalking", false);
                navMeshAgent.destination = transform.position;
            }
        }

        bool IsFront() {
            //RaycastHit _hit;
            Vector3 directionOfPlayer = transform.position - player.position;
            float angle = Vector3.Angle(transform.forward, directionOfPlayer);
            if (Mathf.Abs(angle) > 130 && Mathf.Abs(angle) < 240) {
                Debug.DrawLine(transform.position, player.position, Color.red);
                return true; 
            }
            return false;
        }

    }
}
