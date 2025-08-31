using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class Interact : MonoBehaviour
{
    private IInteractable nearbyObject;
    [SerializeField] private PlayerMovement pm;

    private void OnEnable()
    {
        InteractionManager.OnInteractionEnd += HandleInteractionEnd;
    }

    private void OnDisable()
    {
        InteractionManager.OnInteractionEnd -= HandleInteractionEnd;
    }

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
            pm.canMove = false;
            nearbyObject.Interact();
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }


    private void HandleInteractionEnd()
    {
        pm.canMove = true;
    }
}