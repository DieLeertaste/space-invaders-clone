using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
    }
}
