using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts.Player;
using Tutorial;
using UnityEngine;

public class LoreNPCManager : MonoBehaviour
{
    public Dialogue.Dialogue dlg;
    public int LoreNPCIndex = 1;
    
    public bool isTalking = false;
    private Animator anim;
    private float _targetIsTalking;
    private float _currentIsTalking;
    public float _lerpSpeed = 0.03f;

    public PlayerController player;
    public Animator pAnim;
    public bool interactPressed;
    public bool canContinue;
    public bool canTalk;
    public GameObject questMarker;
    private Transform questTrans;

    public bool tutComplete;
    
    private TutNPCManager npcManager;
    private bool loreDlg1 = false;

    

    private void Start()
    {
        dlg = gameObject.GetComponent<Dialogue.Dialogue>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        questTrans = transform.Find("QuestMarker");
        questMarker = questTrans.gameObject;
        questMarker.SetActive(false);

        npcManager = GameObject.FindWithTag("TutNPC").GetComponent<TutNPCManager>();


    }
    private void Update()
    {
        isTalking = dlg.isTalking;
                
        canContinue = dlg.canContinue;
                
        _targetIsTalking = isTalking ? 1 : 0;
        _currentIsTalking = Mathf.Lerp(_currentIsTalking, _targetIsTalking, _lerpSpeed * Time.deltaTime);
        anim.SetFloat("isTalking", _currentIsTalking);
        interactPressed = player.interactPressed;

        tutComplete = npcManager.tutComplete;

        if (tutComplete && !loreDlg1)
        {
            questMarker.SetActive(true);
            loreDlg1 = true;
        }
                
        if (canTalk && loreDlg1)
        {
            if (interactPressed && !isTalking)
            {
                dlg.StartDialogue(LoreNPCIndex);
                if (questMarker.activeSelf.Equals(true))
                {
                    questMarker.SetActive(false);
                }
            }
            if (interactPressed && canContinue)
            {
                dlg.NextSentence();
            }
        }
    
        
    
    
    }
    
    public void StartConversation()
    {
        dlg.StartDialogue(LoreNPCIndex);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        canTalk = false;
    }
        
}
