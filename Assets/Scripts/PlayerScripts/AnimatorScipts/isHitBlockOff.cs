using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class isHitBlockOff : StateMachineBehaviour
    {
        /// <summary>
        /// turns isHit off on the block when called in the animator
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isHit",false);
            animator.SetBool("isStunned",false);
        }
    
    }
}
