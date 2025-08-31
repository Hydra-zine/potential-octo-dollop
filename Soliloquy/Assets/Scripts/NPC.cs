using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    public void Interact()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if (!DialogueManager.Instance.dialogueIsActive) DialogueManager.Instance.StartDialogue(dialogue);
        else DialogueManager.Instance.DisplayNextSentance();
    }
}
