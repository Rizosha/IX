using UnityEngine;

namespace PlayerScripts.Controls
{
    public class Targeting : MonoBehaviour
    {
        /// <summary>
        /// This script mainly controls the animator component that switches between the cameras. 
        /// </summary>
    
        private PlayerControls target;
        private bool targetCam = false;
        private Animator animator;
        public Animator pAnimator;
    
        private void Awake()
        {
            target = new PlayerControls();
            animator = GetComponent<Animator>();
            pAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        }

        private void OnEnable()
        {
            target.Player.Target.Enable();
        }

        private void OnDisable()
        {
            target.Player.Target.Disable();
        }

        //switches the camera if button pressed
        private void Update()
        {
            target.Player.Target.performed += _ => SwitchState();
        }

        void SwitchState()
        {
            if (targetCam)
            {
                animator.Play("Target Camera");
           
            }else
                animator.Play("Free Look Camera");

            targetCam = !targetCam;
            pAnimator.SetBool("isTargeting",targetCam);
        }

    }
}
