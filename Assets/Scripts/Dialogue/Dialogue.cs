using System.Collections;
using System.Collections.Generic;
using PlayerScripts.Player;
using TMPro;
using UnityEngine;


namespace Dialogue
{
  public class Dialogue : MonoBehaviour
  {
   
    [Header("Conversations",order=1)]
    public string[] firstParagraph;
    public string[] secondParagraph;
    public string[] thirdParagraph;
    public string[] fourthParagraph;
    public string[] fifthParagraph;
    public string[] sixthParagraph;
    public string[] seventhParagraph;
    public string[] eighthParagraph;
    public string[] ninethParagraph;
    public string[] tenthParagraph;
    private string[] currentSentences;
    
    [Header("References",order=2)]
    public GameObject box;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed;
    private Animator player;
    
    [Header("Variables",order=3)]
    private int lineIndex;
    public bool isTalking = false; 
    public bool canContinue = false;
    private bool canGenerate;
    private bool isTyping = false;
    private bool isGenerated = false;
    public Dictionary<int, string[]> paragraphs;
    public bool spokenForFirstTime = true;

    void Start()
    {
      //Creates a dictionary for the stored paragraphs to access 
      paragraphs = new Dictionary<int, string[]>
      {
        { 1, firstParagraph },
        { 2, secondParagraph },
        { 3, thirdParagraph },
        { 4, fourthParagraph },
        { 5, fifthParagraph },
        { 6, sixthParagraph },
        { 7, seventhParagraph },
        { 8, eighthParagraph },
        { 9, ninethParagraph },
        { 10, tenthParagraph }
      };

      player = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
      
      //Check to see if you can generate text and not already typing. Allows you to continue 
      if (canGenerate && !isTyping)
      {
        canContinue = true;
      }
      
      
    }
    
    IEnumerator Type()
    {
      // types out the text at a given speed, has a check to say if typing currently
      isTyping = true;
      foreach (var letter in currentSentences[lineIndex].ToCharArray())
      {
        textDisplay.text += letter;
        yield return new WaitForSeconds(typingSpeed);
      }
      isTyping = false;
    }
    
    IEnumerator TalkCooldown()
    {
      // Waits for 2 seconds after talking for player to talk again 
      yield return new WaitForSeconds(1f);
      isTalking = false;
      player.SetBool("isTalking", false);
    }
 
    public void GenerateSentence()
    {
      // main part for starting a conversation, turns on dialogue box and starts the typing co routine after setting
      // generating variables. These checks stop the multi generation
      if (!isGenerated)
      {
        //Debug.Log("generating");
        isTalking = true;
        player.SetBool("isTalking",true);
        canGenerate = true;
        isGenerated = true;
        box.SetActive(true);
        StartCoroutine(Type());
      }
    }
    
    public void NextSentence()
    {
      // moves onto next dialogue sentence and calls type again if text has finished 
      canContinue = false;
      if (!isTyping && lineIndex < currentSentences.Length - 1)
      {
        lineIndex++;
        textDisplay.text = "";
        StartCoroutine(Type());
      }
      else if (!isTyping)
      {
        //this is the condition if the dialogue has finished. This turns off the box and resets everything along with 
        //starting a talk cooldown
        canGenerate = false;
        textDisplay.text = "";
        box.SetActive(false);
        StartCoroutine(TalkCooldown());
        currentSentences = null;
        lineIndex = 0;
        isGenerated = false;
        spokenForFirstTime = false;
      }
    }
    
    public void GetCurrentSentences(int index)
    {
      //sets current sentences to the paragraph dictionary set
      currentSentences = paragraphs[index];
    }

    public void StartDialogue(int index)
    {
      //call this method with a given index to start a conversation
      GetCurrentSentences(index);
      GenerateSentence();
    }
    
  }
}
