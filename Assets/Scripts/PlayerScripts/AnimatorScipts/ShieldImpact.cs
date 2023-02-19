using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class ShieldImpact : StateMachineBehaviour
    {
        /// <summary>
        /// will set the bool to make the player stagger in blocking phase 
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isHit", false);  
        }

    }
}
