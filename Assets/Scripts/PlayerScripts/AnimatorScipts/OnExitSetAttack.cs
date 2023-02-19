using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class OnExitSetAttack : StateMachineBehaviour
    {
        /// <summary>
        /// sets running attack back to false for no double damage incidents
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("rAttacking", false);
        }
    
    }
}
