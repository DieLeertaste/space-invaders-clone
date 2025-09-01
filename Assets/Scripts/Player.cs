
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    
    public GameObject bulletPrefab;
    public float shootInterval = 5f;
    
    private float _moveDirection;
    private float _minX, _maxX;
    
    private void Start()
    {
        var cam = Camera.main;
        var halfWidth = cam.orthographicSize * cam.aspect;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        var playerHalfWidth = spriteRenderer.bounds.extents.x;
        
        _minX = cam.transform.position.x - halfWidth + playerHalfWidth;
        _maxX = cam.transform.position.x + halfWidth - playerHalfWidth;
        
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDirection * speed, rb.linearVelocity.y);
        
        // Clamp position within screen bounds
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
        transform.position = pos;
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _moveDirection = -1f;
        } else if (context.canceled)
        {
            _moveDirection = 0f;
        }
        
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _moveDirection = 1f;
        } else if (context.canceled)
        {
            _moveDirection = 0f;
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
