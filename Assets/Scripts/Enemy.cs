using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        var bullet = collider2d.gameObject.GetComponent<Bullet>();
        if (bullet == null) return;
        
        var damage = bullet.damage;
        if (health - damage <= 0) Destroy(gameObject);
        else health -= damage;
        Destroy(bullet.gameObject);
    }
}
