/// <summary>
/// controls the conditions from when you have been damaged. Mainly makes it so you can't perform actions in a
/// damaged state
/// </summary>

using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class Damaged : StateMachineBehaviour
    {
       
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isHit",false);
            animator.SetBool("Damaged",true);
            animator.SetBool("isStill",true);
            animator.SetBool("isDodging",false);
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Damaged",false);
            animator.SetBool("isStill",false);
            animator.SetBool("isHit",false);
        }
    
    }
}
