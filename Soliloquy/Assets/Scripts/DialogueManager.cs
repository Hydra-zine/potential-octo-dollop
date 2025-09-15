using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static DialogueManager Instance { get; private set; }
    public bool dialogueIsActive;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject DialogueUI;

    [SerializeField] private GameObject TextCursor;

    [SerializeField] private float letterSpeed = 0.05f;


    private string currentSentance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private Queue<string> sentances;
    void Start()
    {
        sentances = new Queue<string>();
        DialogueUI.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        InteractionManager.StartInteraction();

        dialogueIsActive = true;

        sentances.Clear();

        DialogueUI.SetActive(true);
        TextCursor.SetActive(false);

        nameText.text = dialogue.name;

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        DisplayNextSentance();
    }


    public void DisplayNextSentance()
    {
        

        if (dialogueText.text != currentSentance && !string.IsNullOrEmpty(currentSentance))
        {
            StopAllCoroutines();
            TextCursor.SetActive(true);
            dialogueText.text = currentSentance;
            return;
        }

        if (sentances.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentance = sentances.Dequeue();
        StopAllCoroutines();
        TextCursor.SetActive(false);
        StartCoroutine(TypeSentance(currentSentance));
    }

    IEnumerator TypeSentance(string sentance)
    {
        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
        TextCursor.SetActive(true);
    }

    public void EndDialogue()
    {
        InteractionManager.EndInteraction();
        DialogueUI.SetActive(false);
        dialogueIsActive = false;
    }

}
