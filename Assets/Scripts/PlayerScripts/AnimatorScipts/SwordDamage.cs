using EnemyScripts;
using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class SwordDamage : MonoBehaviour
    {

        /// <summary>
        /// this is a big list of event that are called through some animtions in order to attack once against enemy
        /// </summary>
    
        public LionPlayerDamage lion;
        public bool heavyAttempt;
        public Animator anim;

        private void Start()
        {
            anim = GetComponentInParent<Animator>();
        }
    
        void Combo1On()
        {
            anim.SetBool("isAttacking", true);
        }
        void Combo1Off()
        {
            anim.SetBool("isAttacking", false);
        }

        void isAttacking2On()
        {
            anim.SetBool("isAttacking2",true);
        }

        void isAttacking2Off()
        {
            anim.SetBool("isAttacking2",false);
        }
    
        void HeavyDamageOn()
        {
            anim.SetBool("hAttacking",true);
        }
        void HeavyDamageOff()
        {
            anim.SetBool("hAttacking",false);
        }

        void RunningOn()
        {
            anim.SetBool("rAttacking",true);
        }

        void RunningOff()
        {
            anim.SetBool("rAttacking",false);
        }

        void jAttackingOn()
        {
            anim.SetBool("jAttacking",true);
        }

        void jAttackingOff()
        {
            anim.SetBool("jAttacking",false);
        }

    }
}
