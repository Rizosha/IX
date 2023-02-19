/// <summary>
/// Creates a combo listen command to see if the player had any input to perform the next attack 
/// </summary>

using UnityEngine;
using UnityEngine.Animations;

namespace PlayerScripts.AnimatorScipts
{
    public class OnexitCombo : StateMachineBehaviour
    {
        public bool listen;
        private bool attackPressed;
        private bool performAttack;
    
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           // animator.SetBool("PerformingAttack",true);

        }
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("PerformingAttack",true);
            
            //sets animator and button press bool
            listen = animator.GetBool("comboListen");
            attackPressed = animator.GetBool("attackPressed");
        
            //if pressed a button, perform next combo
            if (listen & attackPressed)
            {
                animator.SetBool("PerformCombo", true);
            }
        }


        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 
            animator.SetBool("isAttacking",false);
            animator.SetBool("PerformingAttack",false);
            animator.SetBool("isStill", false);
        }
    }
}
