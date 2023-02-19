using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class StationaryAttack : StateMachineBehaviour
    {
        /// <summary>
        /// Sets player bool to make the player not move during attacking
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill", true);
        }
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill", false);
        
        }
    }
}
