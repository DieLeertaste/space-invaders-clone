
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    
    public GameObject bulletPrefab;
    public float shootInterval = 5f;
    
    private float moveDirection = 0f;

    void Start()
    {
        InvokeRepeating(nameof(shoot), 0f, shootInterval);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    public void moveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveDirection = -1f;
        } else if (context.canceled)
        {
            moveDirection = 0f;
        }
        
    }

    public void moveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveDirection = 1f;
        } else if (context.canceled)
        {
            moveDirection = 0f;
        }
    }

    public void shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
