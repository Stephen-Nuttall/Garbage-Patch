using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference movementInput;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 1.0f;
    Vector2 moveDirection;
    float defaultXScale;

    void Start()
    {
        defaultXScale = transform.localScale.x;
    }

    void Update()
    {
        // read movement input
        moveDirection = movementInput.action.ReadValue<Vector2>();

        // move the player in the direction they're trying to move in
        rb.linearVelocity = moveDirection * moveSpeed;

        // flip the player to face the direction it is going
        if (moveDirection.x > 0 && Time.timeScale > 0.0f)
            transform.localScale = new Vector3(defaultXScale, transform.localScale.y, transform.localScale.z);
        else if (moveDirection.x < 0 && Time.timeScale > 0.0f)
            transform.localScale = new Vector3(-defaultXScale, transform.localScale.y, transform.localScale.z);
    }
}
