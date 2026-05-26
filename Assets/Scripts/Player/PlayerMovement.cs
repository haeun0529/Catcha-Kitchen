using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Keyboard.current.dKey.isPressed ? 1 :
                  Keyboard.current.aKey.isPressed ? -1 : 0;
        float z = Keyboard.current.wKey.isPressed ? 1 :
                  Keyboard.current.sKey.isPressed ? -1 : 0;

        Vector3 movement = new Vector3(x, 0, z).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}