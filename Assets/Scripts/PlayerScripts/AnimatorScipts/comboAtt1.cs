using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class comboAtt1 : StateMachineBehaviour
    {
        /// <summary>
        /// Sets combo attack listen trigger to false to attack can't be performed again
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("PerformCombo", false);
            animator.SetBool("isStill", true);
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isStill", false);
        }

    }
}
