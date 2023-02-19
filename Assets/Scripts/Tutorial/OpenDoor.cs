/// <summary>
/// This is a script to open the prison cell door. It checks to see if the player is close by and has
/// pressed the interact button. 
/// </summary>

using PlayerScripts.Player;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    private bool doorOpen;
    private bool canOpen;
    private PlayerController pC;
    private bool iPressed;
    private Animator anim;

    private void Start()
    {
        pC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        iPressed = pC.interactPressed;
        
        if (iPressed && canOpen)
        {
            doorOpen = !doorOpen;
        }
        
        anim.SetBool("Open",doorOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
