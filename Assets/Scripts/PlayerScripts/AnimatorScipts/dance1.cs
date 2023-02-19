using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class dance1 : StateMachineBehaviour
    {
        /// <summary>
        /// makes it so the player can't move during an emote
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill",true);
            animator.SetBool("isDodging",false);
            animator.SetBool("isWalking", false);
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill",false);  
        }

    }
}
