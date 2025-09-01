using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int scoreValue = 10;
    
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Bullet")) return;
        
        var bullet = collider2d.gameObject.GetComponent<Bullet>();
        if (bullet == null) return;
        
        var damage = bullet.damage;
        if (health - damage <= 0) Killed();
        else health -= damage;
        Destroy(bullet.gameObject);
    }

    private void Killed()
    {
        ScoreController.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
