/// <summary>
/// For this damage script, it takes the variables from the animator for which attack is thrown out. From here, if hit
/// by the attack and variables are true, call the take damage method and turn the relevant bool to false to prevent
/// damage occuring twice. 
/// </summary>

using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Tutorial
{
    public class TestDummy: MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;
        public HealthBar healthBar;

        private Animator tDummyAnim;
        private Animator pAnimator;
        private bool attacking = false;
        private bool isAttacking2 = false;
        private bool hAttacking = false;
        private bool rAttacking = false;
        private bool jAttacking = false;

        private bool hasAttacked = false;
        private bool hasAttacked2 = false;
        private bool advanceTut1 = false;

        private bool hasAttackedH = false;
        private bool advanceTut2 = false;

        private bool hasAttackedJ = false;
        private bool advanceTut3 = false;

        private bool dummyAttacking;
        private bool pHit;
        [SerializeField]
        private int blockCount = 0;
        private bool advanceTut4 = false;

        private bool hasAttackedR = false;
        public bool advanceTut5 = false;
        

     

        private TutNPCManager tutMan;
        private TestDummySword sword;
        
        
        void Start() {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            pAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
            tutMan = GameObject.FindWithTag("TutNPC").GetComponent<TutNPCManager>();
            tDummyAnim = gameObject.GetComponent<Animator>();
            sword = GameObject.FindWithTag("DummySword").GetComponent<TestDummySword>();
        }

        void Update()
        {
            attacking = pAnimator.GetBool("isAttacking");
            hAttacking = pAnimator.GetBool("hAttacking");
            isAttacking2 = pAnimator.GetBool("isAttacking2");
            rAttacking = pAnimator.GetBool("rAttacking");
            jAttacking = pAnimator.GetBool("jAttacking");

            dummyAttacking = tDummyAnim.GetBool("isAttacking");
            pHit = sword.playerHit;
            

            if (hasAttacked && hasAttacked2 && !advanceTut1)
            {
                tutMan.TutNPCIndex = 2;
                tutMan.questMarker.SetActive(true);
                advanceTut1 = true;
            }

            if (hasAttackedH && !advanceTut2)
            {
                tutMan.TutNPCIndex = 3;
                tutMan.questMarker.SetActive(true);
                advanceTut2 = true;
            }

            if (hasAttackedJ && !advanceTut3)
            {
                tutMan.TutNPCIndex = 4;
                tutMan.questMarker.SetActive(true);
                advanceTut3 = true;
                
                tDummyAnim.SetBool("Attack",true);
            }

            if (dummyAttacking && pHit)
            {
                pAnimator.SetBool("isHit",true);
                tDummyAnim.SetBool("isAttacking",false);
                if (pAnimator.GetBool("isBlocking") && tutMan.questMarker.activeSelf == false)
                {
                    blockCount++;
                    //Debug.Log("should stagger dummy");
                }
            }

            if (blockCount >= 3 && !advanceTut4)
            {
                tutMan.TutNPCIndex = 5;
                tutMan.questMarker.SetActive(true);
                advanceTut4 = true;
            }

            if (hasAttackedR && !advanceTut5)
            {
                tutMan.TutNPCIndex = 6;
                tutMan.questMarker.SetActive(true);
                advanceTut5 = true;
            }


            if (blockCount >= 3)
            {
                tDummyAnim.SetBool("Attack",false);
            }

            if (currentHealth <= 0)
            {
                tDummyAnim.SetBool("isDead",true);
                healthBar.gameObject.SetActive(false);
            }
        }

        public void Combo1On()
        {
           tDummyAnim.SetBool("isAttacking",true); 
        }
        
        public void Combo1Off()
        {
            tDummyAnim.SetBool("isAttacking",false); 
        }
        
        
        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Sword") && attacking && !hasAttacked && tutMan.questMarker.activeSelf == false) {
                TakeSwordDamage(20);
                pAnimator.SetBool("isAttacking",false);
                hasAttacked = true;
            }
            
            if (other.gameObject.CompareTag("Sword") && isAttacking2 && !hasAttacked2 && tutMan.questMarker.activeSelf == false) {
                TakeSwordDamage(20);
                pAnimator.SetBool("isAttacking2",false);
                hasAttacked2 = true;
            }
        
            if (other.gameObject.CompareTag("Sword") && hAttacking && advanceTut1 && !hasAttackedH && tutMan.questMarker.activeSelf == false) {
                TakeSwordDamage(30);
                pAnimator.SetBool("hAttacking",false);
                hasAttackedH = true;
            }
            
            if (other.gameObject.CompareTag("Sword") && jAttacking && advanceTut2 && !hasAttackedJ && tutMan.questMarker.activeSelf == false) {
                TakeSwordDamage(40);
                pAnimator.SetBool("jAttacking",false);
                hasAttackedJ = true;
            }

            if (other.gameObject.CompareTag("Sword") && rAttacking && !hasAttackedR && advanceTut4 && tutMan.questMarker.activeSelf == false ) {
                TakeSwordDamage(30);
                pAnimator.SetBool("rAttacking",false);
                hasAttackedR = true;
            }
        
           
        }
        
        void TakeSwordDamage(int damage){
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}