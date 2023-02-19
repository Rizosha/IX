using UnityEngine;

namespace EnemyScripts.Lion
{
    public class LionDamageState : LionBaseState
    {
        private bool isDead;
        private float timeBeforeTransition = 8f;
        private float timeSinceDeath;
    
        public override void EnterState(LionStateManager lion) { }

        public override void UpdateState(LionStateManager lion) {

            if (lion.damageScript.stun) {
                lion.animator.SetBool("isStunned", true);
                lion.damageScript.stun = false;
            }
            else {
                lion.animator.SetBool("isHit", false);
            }
        
            if (lion.damageScript.currentHealth <= 0) {
                lion.animator.SetBool("isDead",true);
                timeSinceDeath += Time.deltaTime;
                isDead = true;
            }
        
            if (lion.animator.GetBool("isHit") == false & isDead == false) {
                lion.damageScript.hit = false;
                lion.SwitchState(lion.ChaseState);
            }

            if (timeSinceDeath >= timeBeforeTransition) {
                lion.dead = true;
            }
        }

        public override void OnCollisionEnter(LionStateManager lion, Collision collision) { }
    
    }
}
