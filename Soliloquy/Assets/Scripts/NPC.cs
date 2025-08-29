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
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
