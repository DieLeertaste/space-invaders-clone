using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public new Camera camera;
    public new Renderer renderer;
    public Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        if (camera == null) camera = Camera.main;
        renderer = GetComponent<Renderer>();
    }
    
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
    }

    private void Update()
    {
        if (!IsInView()) Destroy(gameObject);
    }

    private bool IsInView()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
