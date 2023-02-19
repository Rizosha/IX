using EnemyScripts.Lion;
using UI;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyDamageScript : MonoBehaviour
    {
        private LionStateManager lion;
        
        public int maxHealth = 300;
        public int currentHealth;
        public HealthBar healthBar;
        public bool hit;
        public bool stun;
    
        public bool attacking = false;
        public bool isAttacking2 = false;
        public bool hAttacking = false;
        public bool rAttacking = false;
        public bool jAttacking = false;
        private Animator pAnimator;

        void Start() {
            lion = GetComponent<LionStateManager>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            pAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            /*if (pAnimator.GetBool("isAttacking") == false) {
            hit = false;
        }
        if (pAnimator.GetBool("hAttacking") == false) {
            hit = false;
        }
        if (pAnimator.GetBool("isAttacking2") == false) {
            hit = false;
        }
        if (pAnimator.GetBool("rAttacking") == false) {
            hit = false;
        }
        if (pAnimator.GetBool("jAttacking") == false) {
            hit = false;
        }*/

            attacking = lion.playerAnimator.GetBool("isAttacking");
            hAttacking = lion.playerAnimator.GetBool("hAttacking");
            isAttacking2 = lion.playerAnimator.GetBool("isAttacking2");
            rAttacking = lion.playerAnimator.GetBool("rAttacking");
            jAttacking = lion.playerAnimator.GetBool("jAttacking");
        
            /*attacking = pAnimator.GetBool("isAttacking");
        hAttacking = pAnimator.GetBool("hAttacking");
        isAttacking2 = pAnimator.GetBool("isAttacking2");
        rAttacking = pAnimator.GetBool("rAttacking");
        jAttacking = pAnimator.GetBool("jAttacking");
        */
        
        }

        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Sword") && attacking) {
                TakeSwordDamage(20);
                pAnimator.SetBool("isAttacking",false);
            }
        
            if (other.gameObject.CompareTag("Sword") && hAttacking) {
                TakeSwordDamage(30);
                pAnimator.SetBool("hAttacking",false);
                stun = true;
            }

            if (other.gameObject.CompareTag("Sword") && isAttacking2) {
                TakeSwordDamage(20);
                pAnimator.SetBool("isAttacking2",false);
            }

            if (other.gameObject.CompareTag("Sword") && rAttacking) {
                TakeSwordDamage(30);
                pAnimator.SetBool("rAttacking",false);
                stun = true;
            }
        
            if (other.gameObject.CompareTag("Sword") && jAttacking) {
                TakeSwordDamage(40);
                pAnimator.SetBool("jAttacking",false);
                stun = true;
            }
        
        }

        /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            TakeSwordDamage(20);
        }
    }*/

        /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Sword"))
        {
            TakeSwordDamage(20);
        }
    }*/

        void TakeSwordDamage(int damage) {
            hit = true;
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}
