using UnityEngine;

namespace PlayerScripts.AnimatorScipts
{
    public class Dodge : StateMachineBehaviour
    {
        /// <summary>
        /// Main animator component for the dodge
        /// This sets the dodge boost to false along with making it so the player can't control movement during dash as this
        /// gave too much freedom
        /// </summary>
   
        public bool dodgeBoost;
        private bool hit;
        private bool vuln;
   
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isDodging", true) ;
            animator.SetBool("isStill", true);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("dodgePressed", false);
        }
    
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isDodging", false);
            
            animator.SetLayerWeight(6,0);
            DodgeOff();
        }

        public void DodgeOn()
        {
            dodgeBoost = true;
        }
    
        public void DodgeOff()
        {
            dodgeBoost = false;
        }

    
    }
}
