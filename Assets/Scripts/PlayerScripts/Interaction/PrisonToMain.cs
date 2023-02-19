/// <summary>
/// Loads Prison to main on trigger
/// </summary>
/// <param name="other"></param>

using System;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts.Interaction
{
    public class PrisonToMain : MonoBehaviour
    {
        public TutorialManager tutMan;
        private TutNPCManager tut;

        private bool tutComplete;

        private Animator pGate;
        private void Start()
        {
            tutMan = GameObject.FindWithTag("GameController").GetComponent<TutorialManager>();
            tut = GameObject.FindWithTag("TutNPC").GetComponent<TutNPCManager>();
            pGate = GameObject.FindWithTag("PrisonGate").GetComponent<Animator>();
        }

        private void Update()
        {
            tutComplete = tut.tutComplete;
            if (tutComplete)
            {
                pGate.SetBool("OpenGate",true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && tutComplete)
            {
                tutMan.CreateTutorialData();
                tutMan.SetPaths();
                tutMan.SaveData();
                SceneManager.LoadScene(4);
            }
            else
            {
                
            }
        }
    }
}
