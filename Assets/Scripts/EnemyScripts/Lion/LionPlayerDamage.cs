using EnemyScripts.Lion;
using PlayerScripts.Player;
using Unity.VisualScripting;
using UnityEngine;
//using PlayerScripts.PlayerStateMachine;

namespace EnemyScripts
{
    public class LionPlayerDamage : MonoBehaviour
    {
        private PlayerController player;
        private Animator lion;
        private Animator playerAnim;
        public bool doDamage;
        
        public bool lionAttack;
    
        void Start() {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            lion = GameObject.FindWithTag("Enemy").GetComponent<LionStateManager>().animator;
            playerAnim = player.animator;
        }

        private void Update() {
            lionAttack = lion.GetBool("attackAttempt");
        }

        public void OnTriggerEnter(Collider other) {
            if (other.GameObject().CompareTag("Player")) {
                if (player.vulnerable & lionAttack) {
                    lion.SetBool("attackAttempt",false);
                    if (doDamage) {
                        player.TakeDamage(10);
                    }
                }

                if (lionAttack) {
                    player.animator.SetBool("isHit",true);
                }

                if (playerAnim.GetBool("isBlocking")) {
                    lion.SetBool("isHit",true);
                    lion.SetBool("isStunned",true);
                }
            }
            
        }
    }
}
