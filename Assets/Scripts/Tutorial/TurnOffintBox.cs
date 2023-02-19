using System.Collections;
using System.Collections.Generic;
using PlayerScripts.Player;
using UnityEngine;

public class TurnOffintBox : MonoBehaviour
{
    private PlayerController player;

    private bool pressedBefore = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.interactPressed && !pressedBefore)
        {
            gameObject.SetActive(false);
            pressedBefore = true;
            player.animator.SetBool("Sleep",false);
        }
    }
}
