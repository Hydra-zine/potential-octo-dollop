using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool collided = false;
    private string eventCase;

    void OnTriggerEnter(Collider other)
    {
        collided = true;
        eventCase = other.tag;
    }

    void OnTriggerExit(Collider other)
    {
        collided = false;
        eventCase = null;
    }

    public void interact(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();

        //Debug.Log(eventCase);

        if (collided && value > 0.3)
        {
            switch (eventCase)
            {
                case "NPC":
                    Debug.Log("NPC moment");
                    break;
                default:
                    Debug.Log("Unknown Interaction");
                    break;

            }
        }



    }
}
