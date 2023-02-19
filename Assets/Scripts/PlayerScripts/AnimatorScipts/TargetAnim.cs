using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class TargetAnim : StateMachineBehaviour
    {

        /// <summary>
        /// Animator component for switching between cameras
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Target", true);
        }
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Target", false);
        }
    }
}
