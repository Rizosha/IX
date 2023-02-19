/// <summary>
/// This is the main movement and controller script for the player.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace PlayerScripts.Player
{
    public class PlayerController : MonoBehaviour
    {
    // movement variables        
        [Header("Character Settings",order=1)]
        [SerializeField]
        private float gravityValue = -9.81f;
        [SerializeField]
        private float rotationSpeed = 4f;
        [SerializeField]
        private float rollDistance = 2f;
        [SerializeField]
        private float walkingSpeed = 1f;
        [SerializeField]
        private float runningSpeed = 2f;
        private float currentSpeed;
        
    // Health        
        [Header("Health and Input smoothing",order=2)]
        public int maxHealth = 100;
        public int currentHealth;

        public int maxStamina = 100;
        public int currentStamina;
        public int lastCurrentStamina;
        public float stamSpeed;
       
        
        
    // Animator Variables   
        private bool isWalking;
        private bool isIdle;
        private bool isTurning = false;
        private Vector2 DirectInput;
        private Vector3 velocity;
        
    // Smooth the input and angle variables
        private Vector2 smoothedInput;
        private Vector2 smoothInputValue;
        private Vector2 runInput;
        private Vector2 currentInputVector;
        private float lastInputAngle;
        [SerializeField]
        private float smoothInputSpeed = 0.1f;
        
    // Dodge variables       
        private Vector3 rollDirection;
        private Vector3 attackVeloc;
        bool dodgeBoost;
        bool iFrame;
        bool isBlocking;
        [HideInInspector]
        public bool vulnerable;
        
    // Block layer Variable        
        float layerSmoothTime = 0.03f;
        
    // Buttons press bools        
        bool targetTogglePressed;
        bool targetToggle;
        [SerializeField]
        public bool interactPressed;
        bool performingAttack; 
        bool dodgePressed;
        bool emote1Pressed;
        bool emote2Pressed;
        bool emote3Pressed; 
        bool emote4Pressed;
        private bool runPressed;

    // Manual references        
        [Header("References",order=3)]
        public Transform cameraMainTransform;
        [HideInInspector]
        public CharacterController controller;
        [HideInInspector]
        public Animator animator;
        public HealthBar healthBar;
        public HealthBar staminaBar;
        private PlayerControls input;
        public Animator camAnim;
   
    //Potion reference
        public GameObject potion;
        [HideInInspector]
        public bool potionOn = true;
        
    // Dialogue variables        
       // private GameObject npcObj;
       [SerializeField]
        private Dialogue.Dialogue dlg;
        [SerializeField]
        private bool isTalking;
        //private bool canContinue = false;
        [SerializeField]
        private GameObject dlgBox;
        
        
      
        private void Awake()
        { 
        // Enable player controls
            input = new PlayerControls();
            input.Player.Enable();
 
        // Grabs components            
            controller = gameObject.GetComponent<CharacterController>();
            animator = gameObject.GetComponent<Animator>();
            
            
        }

        public void Start()
        {
        //sets health
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            
            /*
            //sets stamina
            currentStamina = maxStamina;
            staminaBar.SetMaxHealth(maxStamina);
            */

            //Set Cursor to not be visible
            Cursor.visible=false;
            Cursor.lockState=CursorLockMode.Locked;
           
        }

        public void Update()
        {
        //Potion Health reset
            if (currentHealth > maxHealth) { currentHealth = maxHealth; }
            animator.SetBool("Vulnerable",vulnerable);
            
        //Kills the player
            if (currentHealth <= 0)
            {
                animator.SetBool("isDead",true);
                animator.SetBool("isStill",true);
            }
            
            runPressed = input.Player.Run.IsPressed();
           // animator.SetBool("isTalking", isTalking);
            
            /*if (dlg != null)
            {
                isTalking = dlg.isTalking;
            }*/

          
            isTalking = animator.GetBool("isTalking");
            /*
            staminaBar.SetHealth(currentStamina);
            if (lastCurrentStamina <= currentStamina)
            {
                currentStamina = Mathf.RoundToInt(Mathf.MoveTowards(currentStamina, maxStamina, stamSpeed * Time.deltaTime));

                //RestoreStamina();
            }

            lastCurrentStamina = currentStamina;
            */

            Dash();
            Attack();
            Targeting();
            Interact();
            Taunt();
        }
        
        
        /*
        private IEnumerator IncreaseStaminaOverTime()
        {
            
                yield return new WaitForSeconds(1f); // Wait for 1 second
          
            
        }
        
        public void RestoreStamina()
        {
            StartCoroutine(IncreaseStaminaOverTime());
        }
        */
        
        
        private void FixedUpdate()
        {
            if (animator.GetBool("isStill") == false)
            {
                Movement();
            }
        }
       

        public void Movement()
        {
        // inefficient way of making the character not move if talking. 
            if (isTalking)
            {
                DirectInput = new Vector3(0, 0, 0);
            }
            else
            {
            // Take input from player input system             
                DirectInput = input.Player.Movement.ReadValue<Vector2>();
            }
            
            runInput = DirectInput * 2;
            smoothedInput = Vector2.SmoothDamp(smoothedInput, currentInputVector, ref smoothInputValue, smoothInputSpeed);

        // Convert to Vector3 and use camera's current angle
            velocity = new Vector3(smoothedInput.x, 0, smoothedInput.y);
            velocity = cameraMainTransform.forward * velocity.z + cameraMainTransform.right * velocity.x;
            velocity.y = 0f;
            
        // Apply gravity
            velocity.y += gravityValue * Time.deltaTime;

        // Rotation and walking
            HandleRotation(DirectInput, smoothedInput, velocity);

            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isIdle", isIdle);

        // Sets the movement speed based on if run pressed            
            if (runPressed && velocity.magnitude >= 0.1f)
            {
                animator.SetBool("isRunning", true);
                currentSpeed = runningSpeed;
            }
            else
            {
                animator.SetBool("isRunning", false);
                currentSpeed = walkingSpeed;
            }
 
        // Set to idle if no input
            if (velocity.magnitude == 0f)
            {
                isIdle = true;
            }
            else
            {
                isIdle = false;
            }
            
        // Set animator components to the smoothed input 
            animator.SetFloat("VelocityZ", smoothedInput.y);
            animator.SetFloat("VelocityX", smoothedInput.x);
            
        // Move the character controller
            controller.Move(velocity.normalized * currentSpeed * Time.deltaTime);
        
        }

        private void HandleRotation(Vector2 DirectInput, Vector2 smoothedInput, Vector3 velocity)
        {
             
             if (DirectInput != Vector2.zero)
            {
            // create a new angle based of the input + camera and set rotation    
                float targetAngle = Mathf.Atan2(smoothedInput.x, smoothedInput.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

           // Grab current angle and compare it with last angle. If angle is 180, set animator to do 180
                float currentInputAngle = targetAngle;
                float angleDifference = Mathf.Abs(currentInputAngle - lastInputAngle);
                if (angleDifference >= 120 && angleDifference <= 320 && !isTurning)
                {
                    animator.SetBool("Turn", true);
                }
                lastInputAngle = currentInputAngle;

                #region NothingToSeeHere

                    /*// create a new angle based of the input + camera and set rotation    
                                float targetAngle = Mathf.Atan2(DirectInput.x, DirectInput.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
                                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                
                                // Grab current angle and compare it with last angle. If angle is 180, set animator to do 180
                               // float currentForwardDirection = transform.forward.y;
                                float currentInputAngle = transform.rotation.y;
                                
                                float angleDifference = Mathf.Abs(currentInputAngle - lastInputAngle);
                                if (angleDifference >= 160 && angleDifference <= 320 && !isTurning)
                                {
                                    animator.SetBool("Turn", true);
                                    Debug.Log("180 Turn " + angleDifference);
                                }
                                else if (currentInputAngle > lastInputAngle && angleDifference > 20)
                                {
                                    Debug.Log("Turning Right " + angleDifference);
                                }
                                else if (currentInputAngle < lastInputAngle && angleDifference > 20)
                                {
                                    Debug.Log("Turning Left " + angleDifference);
                                }
                                lastInputAngle = currentInputAngle;*/
                                
                                /*
                                // create a new angle based of the input + camera and set rotation    
                                float targetAngle = Mathf.Atan2(smoothedInput.x, smoothedInput.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
                                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                
                                // Grab current angle and compare it with last angle. If angle is 180, set animator to do 180
                                float currentInputAngle = targetAngle;
                                float angleDifference = Mathf.Abs(currentInputAngle - lastInputAngle);
                
                                float currentForwardDirection = transform.forward.y;
                                float angleToCurrentForwardDirection = currentInputAngle - currentForwardDirection;
                
                                if (angleDifference >= 120 && angleDifference <= 320 && !isTurning)
                                {
                                    animator.SetBool("Turn", true);
                                    if (angleToCurrentForwardDirection >= 0 && angleToCurrentForwardDirection <= 180)
                                    {
                                        Debug.Log("Turned Right");
                                    }
                                    else if (angleToCurrentForwardDirection >= 180 && angleToCurrentForwardDirection <= 360)
                                    {
                                        Debug.Log("Turned Left");
                                    }
                                }
                                else if (angleDifference >= 160 && angleDifference <= 200)
                                {
                                    Debug.Log("Turned 180");
                                }
                                lastInputAngle = currentInputAngle;
                                */

                    #endregion
                    
            // Sets variables, i think unused + need to check    
                isWalking = true;
                rollDirection = new Vector3(velocity.x, 0f, velocity.z);
            }
            else
            {
                isWalking = false;
                isIdle = true;
                
            }
             
        // Sets the value of which turn to perform             
            if (runPressed)
            {
                currentInputVector = runInput;
                animator.SetFloat("TurnValue", 1);
            }
            else
            {
                currentInputVector = DirectInput;
                animator.SetFloat("TurnValue", 0);
            }
        }
        
        void Block()
        {
            bool blockPressed = input.Player.Block.IsPressed();
            animator.SetBool("Blocking",blockPressed);

            if (!isTalking)
            {
                // Set the layer weight of block with a smooth damp. 
                var blockingLayerWeight = animator.GetLayerWeight(1);
                blockingLayerWeight = Mathf.SmoothDamp(blockingLayerWeight, blockPressed ? 1 : 0, ref blockingLayerWeight, layerSmoothTime);
                animator.SetLayerWeight(1,blockingLayerWeight);
                float blockWeight = animator.GetLayerWeight(1);
                animator.SetBool("isBlocking",isBlocking);

                // Sets the player into a blocking state if fully blocking, if not they become vulnerable to attacks
                if (blockWeight >= 1f)
                {
                    isBlocking = true;
                }
                else
                {
                    isBlocking = false;
                    vulnerable = true;
                }
            }
     
        }
    
        void Dash()
        {
        //input and reference
            bool isDodging = animator.GetBool("isDodging");
            dodgePressed = input.Player.Dodge.IsPressed();
            animator.SetBool("dodgePressed", dodgePressed);
            animator.SetLayerWeight(6,1);
  
        //gives the player a boost during dodge
            if (dodgeBoost && !targetToggle)
            {
                controller.Move(transform.forward * rollDistance * Time.deltaTime);
            }
            if (dodgeBoost && targetToggle)
            {
                controller.Move(rollDirection * rollDistance * Time.deltaTime);
            }
        
        //sets the player to vulnerable if no Iframes available
            if (iFrame)
            {
                vulnerable = false;
            }
        
        //No damage if blocking
            if (isBlocking)
            {
                vulnerable = false;
            }
        }
    
        public void DodgeBoolOff()
        {
            animator.SetBool("isDodging",false);
        }

        void Attack()
        {
        //input
            bool attackPressed = input.Player.Attack.IsPressed();
            bool HattackPressed = input.Player.HeavyAttack.IsPressed();
            bool jAttackPressed = input.Player.JumpAttack.IsPressed();
        
        //animator ref 
            animator.SetBool("attackPressed", attackPressed);
            animator.SetBool("hAttackPressed",HattackPressed);
            animator.SetBool("jumpAttackPressed",jAttackPressed);

        //sets the player to still if attacking, also makes it so the player cant block during an attack
            performingAttack = animator.GetBool("PerformingAttack");
            if (performingAttack)
            {
                animator.SetBool("isStill",true);
                animator.SetLayerWeight(1,0);
            }
            else
            {
                Block();
            }
        }
        
    //Animation events 
        public void TurnOn()
        {
            isTurning = true;
        }

        public void TurnOff()
        {
            isTurning = false;
            animator.SetBool("Turn",false);
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        
        void Interact()
        {
            interactPressed = input.Player.Interact.WasPressedThisFrame();

        //starts a conversation
            /*if (dlg != null)
            {
                if (interactPressed && !isTalking)
                {
                    dlg.StartDialogue(1);
                }

                if (interactPressed && canContinue)
                {
                    dlg.NextSentence();
                }
            }*/
        }
  
    //gives the player health 
        public void GiveHealth(int health)
        {
            potionOn = true;
            currentHealth += health;
            healthBar.SetHealth(currentHealth);
            potion.SetActive(false);
        }

        public void TakeStamina()
        {
            
        }

        public void Targeting()
        {
        //grabs the animator bool 
            targetToggle = camAnim.GetBool("Target");
      
        //sets player animator bool to cam bool 
            animator.SetBool("isTargeting",targetToggle);

            float toggleWeight = animator.GetLayerWeight(2);

            #region NothingToSeeHere

                 //changes the layer weight into strafe movement 
            
                        /*if (targetToggle)
                    {
                        animator.SetLayerWeight(2,1);
                    }
                    else
                    {
                        animator.SetLayerWeight(2,0);
                    }*/
                        
                    //face the target if target toggle is true 
                        /*if (toggleWeight == 1)
                    {
                        Vector3 targetDist = enemy.transform.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(targetDist );
                        transform.rotation = rotation;
                    }*/

            #endregion
   
        }

        void Taunt()
        {
        //Emote buttons pressed
            emote1Pressed = input.Player.Emote1.IsPressed();
            emote2Pressed = input.Player.Emote2.IsPressed();
            emote3Pressed = input.Player.Emote3.IsPressed();
            emote4Pressed = input.Player.Emote4.IsPressed();
      
        //Set emote animator bool 
            animator.SetBool("emote1Pressed",emote1Pressed);
            animator.SetBool("emote2Pressed",emote2Pressed);
            animator.SetBool("emote3Pressed",emote3Pressed);
            animator.SetBool("emote4Pressed",emote4Pressed);
        }
    
    // Set Triggers to grab dialogue components if NPC near. 
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("TutNPC"))
            {
                //npcObj = other.gameObject;
                dlg = other.GetComponent<Dialogue.Dialogue>();
               // isTalking = dlg.isTalking;
               // canContinue = dlg.canContinue;
            }
            if (other.gameObject.CompareTag("LoreNPC"))
            {
                //npcObj = other.gameObject;
                dlg = other.GetComponent<Dialogue.Dialogue>();
                //isTalking = dlg.isTalking;
                //canContinue = dlg.canContinue;
            }
        }
    
    // Set components to null if out of range    
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("TutNPC"))
            {
                //npcObj = null;
                dlg = null;
                //isTalking = false;
                //canContinue = false;
            }
            if (other.gameObject.CompareTag("LoreNPC"))
            {
                //npcObj = null;
                dlg = null;
                //isTalking = false;
                //canContinue = false;
            }
        }

        #region Dodge Toggle
    
        public void DodgeOn()
        {
            dodgeBoost = true;
            currentStamina -= 40;
        }
    
        public void DodgeOff()
        {
            dodgeBoost = false;
            animator.SetBool("isStill", false);
            
        }

        #endregion
        
        #region Iframe Toggle

        void iFrameOn()
        {
            iFrame = true;
        }
      
        void iframeOff()
        {
            iFrame = false;
            animator.SetBool("isHit", false);
        }
        
        #endregion
        
        #region Combo Toggle

        void comboOn()
        {
            animator.SetBool("comboListen", true);
        }
    
        void comboOff()
        {
            animator.SetBool("comboListen", false);
        }

        #endregion
    }
}

