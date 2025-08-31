using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public bool canMove = true;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] Rigidbody rb;
    private Vector3 deltaPos = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity = new Vector3(deltaPos.x*speed, -fallSpeed, deltaPos.y*speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        var value = context.ReadValue<Vector2>();
        deltaPos = new Vector2(value.x, value.y).normalized;

        //Debug.Log(deltaPos);

        if (!canMove) return;

        if (deltaPos.x < -math.EPSILON)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
        else if (deltaPos.x > math.EPSILON)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }
}
