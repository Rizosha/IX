using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class deathStill : StateMachineBehaviour
    {
        /// <summary>
        /// used to make the player still when dead, but for some reason doesn't call when uncommented
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //animator.SetBool("isStill",true);
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
      
        }
    
    }
}
