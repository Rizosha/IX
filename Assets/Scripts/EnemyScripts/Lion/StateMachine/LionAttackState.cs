using UnityEngine;
using UnityEngine.Rendering.UI;

namespace EnemyScripts.Lion
{
    public class LionAttackState : LionBaseState
    {
        private float rotationSpeed = 5f;
        private Quaternion _lookRotation;
        private Vector3 _direction;
        private float timeBeforeMove = 1f;
        private float timeSinceMove;
        private float timeBeforeAttack = 1f;
        private float timeSinceAttack;
        private int choice;

        public override void EnterState(LionStateManager lion) {
            timeSinceMove = 0;
            timeSinceAttack = 0;

            if (IsFront(lion) && IsClose(lion)) {
                lion.animator.SetBool("isAttacking", true);

                choice = Random.Range(1,3);
                lion.animator.SetInteger("AttackChoice", choice);
            }
        }

        public override void UpdateState(LionStateManager lion) {

            timeSinceAttack += Time.deltaTime;
            
            if (IsClose(lion) && IsFront(lion) == false) {
                _direction = (lion.player.position - lion.transform.position).normalized;
                //create the rotation we need to be in to look at the target
                _lookRotation = Quaternion.LookRotation(_direction);
                //rotate us over time according to speed until we are in the required rotation
                lion.transform.rotation = Quaternion.Slerp(lion.transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            }
            

            if (IsFront(lion) && IsClose(lion)) { 
                if (timeSinceAttack > timeBeforeAttack){
                    lion.animator.SetBool("isAttacking", true);
                    
                    choice = Random.Range(1, 3);
                    lion.animator.SetInteger("AttackChoice", choice);
                    
                    timeSinceAttack = 0;
                }
            }
            else {
                lion.animator.SetBool("isAttacking", false);
            }
            
            if (IsClose(lion) == false) {
                timeSinceMove += Time.deltaTime;
                if (timeSinceMove > timeBeforeMove) {
                    lion.animator.SetBool("isAttacking", false);
                    lion.SwitchState(lion.ChaseState);
                }
            }
        
            if (lion.damageScript.hit) {
                lion.animator.SetBool("isHit", true);
                lion.animator.SetBool("isAttacking", false);
                lion.damageScript.hit = false;
                lion.SwitchState(lion.DamageState);
            }
        }

        public override void OnCollisionEnter(LionStateManager lion, Collision collision) { }
    
        bool IsFront(LionStateManager lion) {
            Vector3 directionOfPlayer = lion.transform.position - lion.player.position;
            float angle = Vector3.Angle(lion.transform.forward, directionOfPlayer);
            if (Mathf.Abs(angle) > 160 && Mathf.Abs(angle) < 200) {
                return true; 
            }
            return false;
        }

        bool IsClose(LionStateManager lion) {
            if (Vector3.Distance(lion.transform.position, lion.player.transform.position) < lion.stopDistance) {
                return true; 
            }
            return false;
        }
    }
}
