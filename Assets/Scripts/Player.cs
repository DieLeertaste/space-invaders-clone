
using System;
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
    
    public int health = 500;
    public TMPro.TMP_Text healthDisplay;
    
    private void Start()
    {
        SetUpBorders();
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Enemy-Bullet")) return;
        var bullet = collider2d.GetComponent<Bullet>();

        if (health - bullet.damage <= 0)
        {
            // Game Over
        }
        else
        {
            health = health - bullet.damage;
        }
        
        Destroy(bullet.gameObject);
    }
    
    private void SetUpBorders()
    {
        var cam = Camera.main;
        var halfWidth = cam.orthographicSize * cam.aspect;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        var playerHalfWidth = spriteRenderer.bounds.extents.x;
        
        _minX = cam.transform.position.x - halfWidth + playerHalfWidth;
        _maxX = cam.transform.position.x + halfWidth - playerHalfWidth;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDirection * speed, rb.linearVelocity.y);
        
        // Clamp position within screen bounds
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
        transform.position = pos;
        
        healthDisplay.text = $"Health: {health}";
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
