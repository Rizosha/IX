using UnityEngine;

namespace EnemyScripts.Lion
{
    public class LionChaseState : LionBaseState
    {
        private float maxTime = 0.5f;
        private float timer = 0.0f;
        private float speed = 3f;
        private float speedModifier;

        public override void EnterState(LionStateManager lion) {
            lion.navMeshAgent.enabled = true;
            
            if (Vector3.Distance(lion.transform.position, lion.player.transform.position) <= 10f) {
                speedModifier = 1; 
                lion.navMeshAgent.speed = speed;
            }
        }

        public override void UpdateState(LionStateManager lion) {
            Debug.DrawLine(lion.transform.position, lion.player.position, Color.blue);

            timer -= Time.deltaTime;
            if (timer < 0.0f) {
                lion.navMeshAgent.destination = lion.player.position;
                timer = maxTime;
            }
        
            if (Vector3.Distance(lion.transform.position, lion.player.transform.position) > 10f) {
                speedModifier = 2;
                lion.navMeshAgent.speed = speed * 2;
            }
            
            lion.animator.SetFloat("Speed", lion.navMeshAgent.velocity.magnitude * speedModifier); //calculate our own magnitude

            if (Vector3.Distance(lion.transform.position, lion.player.transform.position) <= lion.stopDistance) {
                lion.animator.SetFloat("Speed", 0);
                lion.SwitchState(lion.AttackState);
            }

            if (lion.damageScript.hit) {
                lion.animator.SetBool("isHit", true);
                lion.animator.SetFloat("Speed", 0);
                lion.navMeshAgent.enabled = false;
                lion.damageScript.hit = false;
                lion.SwitchState(lion.DamageState);
            }
        }

        public override void OnCollisionEnter(LionStateManager lion, Collision collision) { }
    
        /*bool IsFront(LionStateManager lion) {
        Vector3 directionOfPlayer = lion.transform.position - lion.player.position;
        float angle = Vector3.Angle(lion.transform.forward, directionOfPlayer);
        if (Mathf.Abs(angle) > 130 && Mathf.Abs(angle) < 240) {
            Debug.DrawLine(lion.transform.position, lion.player.position, Color.red);
            return true; 
        }
        return false;
    }*/
    }
}
