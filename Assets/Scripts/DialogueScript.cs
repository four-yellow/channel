using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public float typingSpeed = 0.5f;
    public TextMeshProUGUI dialogueText; //text displayed in the bubble

    public GameObject speechBubble;
    public GameObject continueButton;
    public bool dialogueStarted;

    public bool speakable;

    GameObject player;
    UseKey keyScript;

    GameObject parent;
    ParentMove parentScript;
    public bool parentZone;

    private string[] sentences; // array of sentences to be used

    [TextArea]
    public string[] sentences1; // array of sentences
    [TextArea]
    public string[] sentences2; // array of sentences

    public int dialogueIndex; // which sentence you're on

    private void Start()
    {
        sentences = sentences1;

        player = GameObject.Find("Player");
        parent = GameObject.Find("parent");

        keyScript = player.GetComponent<UseKey>();
        speakable = keyScript.parentSpeakable;

        parentScript = parent.GetComponent<ParentMove>();
        parentZone = parentScript.parentInZone2;
    }
    //starts the speech bubble if you press space
    private void Update()
    {
        speakable = keyScript.parentSpeakable;
        parentZone = parentScript.parentInZone2;

        if (parentZone == true)
        {
            sentences = sentences2;
        }
        else
        {
            sentences = sentences1;
        }


        if (Input.GetKeyDown(KeyCode.Space) && dialogueStarted == false && speakable)
        {
            speechBubble.SetActive(true);
            StartDialogue();
            dialogueStarted = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && dialogueStarted == true && speakable)
        {
            ContinueDialogue();
        }
    }

    public void StartDialogue()
    {
        StartCoroutine("TypeDialogue");
    }

    //prints out letters of a sentence one by one
    private IEnumerator TypeDialogue()
    {
        foreach (char letter in sentences[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    //increments which sentence you're on when you press continue, and closes the bubble if done
    public void ContinueDialogue()
    {
        continueButton.SetActive(false);

        if (dialogueIndex < sentences.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine("TypeDialogue");
        }
        else
        {
            dialogueText.text = string.Empty;
            speechBubble.SetActive(false);
            dialogueStarted = false;
            dialogueIndex = 0;
        }
    }
}
