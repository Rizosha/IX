using EnemyScripts.Lion;
using UI;
using UnityEngine;

namespace EnemyScripts.Oldscript
{
    public class EnemyScript : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;
        public HealthBar healthBar;
        public LionStateManager lion;
    

        void Start() {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        // Update is called once per frame
        void Update()
        {
            if (currentHealth <= 0) {
                lion.animator.SetBool("isDead",true);
                lion.navMeshAgent.enabled = false;
                lion.SwitchState(lion.IdleState);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword"))
            {
                TakeSwordDamage(20);
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

        void TakeSwordDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}
