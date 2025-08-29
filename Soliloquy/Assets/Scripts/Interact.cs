using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    private IInteractable nearbyObject;

    private void OnTriggerEnter(Collider other)
    {
        nearbyObject = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() == nearbyObject)
        {
            nearbyObject = null;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (nearbyObject != null)
        {
            nearbyObject.Interact();
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }
}