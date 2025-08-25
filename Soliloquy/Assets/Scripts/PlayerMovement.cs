using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector3 deltaPos = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += deltaPos * speed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        deltaPos = new Vector3(value.x, 0, value.y);
    }
}
