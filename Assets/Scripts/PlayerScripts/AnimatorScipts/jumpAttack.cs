using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class jumpAttack : StateMachineBehaviour
    {
        /// <summary>
        /// sets attacking bools on and off along with turning movement back on
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("PerformingAttack",true);
        }

   
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill", false);
            animator.SetBool("jAttacking", false);
            animator.SetBool("PerformingAttack",false);
        }
    }
}
