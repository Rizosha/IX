using UnityEngine;

namespace PlayerScripts.Unused
{
    public class PlayerCameraSwitch : MonoBehaviour
    {
        private PlayerControls target;
        private bool targetCam = false;
        private Animator animator;

        private void Awake()
        {
            target = new PlayerControls();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            target.Player.Target.Enable();
        }

        private void OnDisable()
        {
            target.Player.Target.Disable();
        }

        private void Start()
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
        }
    
    }
}
