using System;
using PlayerScripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class TutNPCManager : MonoBehaviour
    {
        public Dialogue.Dialogue dlg;
        public int TutNPCIndex = 1;
        
        public bool isTalking = false;
        private Animator anim;
        private float _targetIsTalking;
        private float _currentIsTalking;
        public float _lerpSpeed = 0.03f;

        [SerializeField]
        private GameObject playerGO;
        public PlayerController player;
        public Animator pAnim;
        public bool interactPressed;
        public bool canContinue;
        public bool canTalk;
        //[SerializeField]
        private GameObject plocation;
        private Vector3 plocationV;
        
        public GameObject questMarker;
        private Transform questTrans;

        public bool tutComplete = false;
        private TestDummy testDummy;
        [SerializeField]
        private bool spokeForFIrstTime;

        private NavMeshAgent agent;
        [SerializeField]
        private GameObject location;
        private Vector3 locationV;
        private bool destR = false;
        [SerializeField]
        private GameObject psword;
        [SerializeField]
        private GameObject pshield;
        

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            pAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
            playerGO = GameObject.FindWithTag("Player");
            

        }

        private void Start()
        {
            dlg = gameObject.GetComponent<Dialogue.Dialogue>();
            anim = gameObject.GetComponent<Animator>();
          
            questTrans = transform.Find("QuestMarker");
            questMarker = questTrans.gameObject;
            testDummy = GameObject.FindWithTag("Enemy").GetComponent<TestDummy>();

            locationV = location.transform.position;
            
            
             if (!tutComplete)
            {
                dlg.StartDialogue(10);
                pAnim.SetBool("Sleep",true);
                pshield.SetActive(false);
                psword.SetActive(false);
            }else 
            {
                gameObject.transform.position = locationV;
            }

          

            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            isTalking = dlg.isTalking;
            
            canContinue = dlg.canContinue;
            
            _targetIsTalking = isTalking ? 1 : 0;
            _currentIsTalking = Mathf.Lerp(_currentIsTalking, _targetIsTalking, _lerpSpeed * Time.deltaTime);
            anim.SetFloat("isTalking", _currentIsTalking);
            interactPressed = player.interactPressed;
            
            if (canTalk)
            {
                if (interactPressed && !isTalking)
                {
                    dlg.StartDialogue(TutNPCIndex);
                    if (questMarker.activeSelf.Equals(true))
                    {
                        questMarker.SetActive(false);
                    }
                }
            }
            
            if (interactPressed && canContinue)
            {
                dlg.NextSentence();
            }
            
            
            if (testDummy.advanceTut5 && questMarker.activeSelf == false)
            {
                tutComplete = true;
            }

            spokeForFIrstTime = dlg.spokenForFirstTime;
            if (!spokeForFIrstTime && !destR && !tutComplete)
            {
                agent.SetDestination(locationV);
                anim.SetFloat("isWalking",1);
                if (!agent.pathPending && agent.remainingDistance < 0.1f && !agent.hasPath)
                {
                    anim.SetFloat("isWalking",0);
                    anim.SetBool("Turn",true);
                    destR = true;
                }
            }
            
        }

        public void StartConversation()
        {
            dlg.StartDialogue(TutNPCIndex);
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
}
