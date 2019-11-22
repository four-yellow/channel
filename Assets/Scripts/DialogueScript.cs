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

    [TextArea]
    public string[] sentences; // array of sentences

    public int dialogueIndex; // which sentence you're on

    //starts the speech bubble if you press space
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speechBubble.SetActive(true);
            StartDialogue();
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
            speechBubble.SetActive(false);
        }
    }
}
