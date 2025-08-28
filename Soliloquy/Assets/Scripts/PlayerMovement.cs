using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] Rigidbody rb;
    private Vector3 deltaPos = Vector3.zero;


    // Update is called once per frame
    void Update()
    {

        rb.linearVelocity = new Vector3(deltaPos.x*speed, -fallSpeed, deltaPos.y*speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        deltaPos = new Vector2(value.x, value.y).normalized;

        //Debug.Log(deltaPos);

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
