/// <summary>
/// sets perfoming attack bool and restricts movement during attack 
/// </summary>

using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class runSlashOff : StateMachineBehaviour
    {
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
    
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("PerformingAttack",true);
            animator.SetFloat("VelocityX", 0);
            animator.SetFloat("VelocityZ", 0);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("rAttacking",false);
            animator.SetBool("PerformingAttack",false);
            animator.SetBool("isStill", false);
        }

    }
}
