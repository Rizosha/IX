/// <summary>
/// Performs the attacking part of the second combo in terms of damaging enemy
/// Sets the combo parameter to false to ignore input form player   
/// </summary>

using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class SecondAttack : StateMachineBehaviour
    {
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("PerformCombo",false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
            animator.SetBool("PerformingAttack",true);
        }


        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isAttacking2",false);
            animator.SetBool("PerformingAttack",false);
            animator.SetBool("isStill", false);
        }
    }
}
