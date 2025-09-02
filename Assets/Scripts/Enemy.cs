using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int scoreValue = 10;
    public float moveSpeed = 5f;
    public float shootInterval = 2f;
    public GameObject bulletPrefab;
    
    private GameObject _playerGameObject;

    private void Start()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(Shoot), 0f, shootInterval);
    }

    private void FixedUpdate()
    {
        if (_playerGameObject) MoveToPlayer();
    }
    
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Bullet")) return;
        
        var bullet = collider2d.gameObject.GetComponent<Bullet>();
        if (bullet == null) return;
        
        var bulletDamage = bullet.damage;
        if (health - bulletDamage <= 0) Killed();
        else health -= bulletDamage;
        Destroy(bullet.gameObject);
    }

    private void Killed()
    {
        ScoreController.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        if (PauseController.instance.IsPaused) return;
        
        var direction = (_playerGameObject.transform.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        if (PauseController.instance.IsPaused) return;
        
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
