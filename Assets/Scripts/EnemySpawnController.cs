using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Header("Spawn Settings")] 
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    public int maxEnemies = 10;

    [Header("Wave System")] 
    public bool useWaveSystem = false;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 10f;
    
    private List<GameObject> _activeEnemies = new List<GameObject>();
    private Coroutine _spawnCoroutine;
    private int _currentWave = 0;
    private bool _isSpawning = false;

    private void Start()
    {
        if (enemyPrefabs.Length == 0) Debug.LogError("No enemy prefabs assigned!");
        StartSpawning();
    }
    private void StartSpawning()
    {
        _isSpawning = true;
        _spawnCoroutine = StartCoroutine(useWaveSystem ? SpawnWaves() : SpawnContinues());
    }
    private void StopSpawning()
    {
        _isSpawning = false;
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }
    private IEnumerator SpawnWaves()
    {
        while (_isSpawning)
        {
            _currentWave++;
            Debug.Log("Starting Wave " + _currentWave);
        
            for (var i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitUntil(() => _activeEnemies.Count == 0);
            yield return new WaitForSeconds(timeBetweenWaves);

            spawnRate = Mathf.Max(0.5f, spawnRate * 0.9f);
        }
    }
    private IEnumerator SpawnContinues()
    {
        while (_isSpawning)
        {
            if (_activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            CleanUpDestroyedEnemies();
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private void CleanUpDestroyedEnemies()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);
    }
    private void SpawnEnemy()
    {
        var spawnPosition = GetSpawnPosition();
        var enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        _activeEnemies.Add(enemy);
    }
    private Vector3 GetSpawnPosition()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
        return spawnPoint.position;
    }
}
