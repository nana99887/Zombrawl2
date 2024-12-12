using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public Transform player;
    public Transform spawnerParent;

    public float spawnRadius = 100f;
    public int maxEnemies = 15;
    public int maxCoins = 20;

    private float spawnTimeEnemy = 10f;
    private float spawnTimeCoin = 4f;

    private int currentEnemyCount = 0; 
    private int currentCoinCount = 0;   

    private void Start()
    {
        SpawnEnemy();
        SpawnCoin();
        InvokeRepeating("SpawnEnemy", 0f, spawnTimeEnemy); 
        InvokeRepeating("SpawnCoin", 0f, spawnTimeCoin);   
    }

    private void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies) return; 

        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity, spawnerParent);
        currentEnemyCount++;
    }

    private void SpawnCoin()
    {
        if (currentCoinCount >= maxCoins) return; 

        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        randomSpawnPosition.y =player.transform.position.y;
        Instantiate(coinPrefab, randomSpawnPosition, Quaternion.identity, spawnerParent);
        currentCoinCount++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;
        NavMeshHit hit;

     
        do
        {
            Vector2 randomCircle = Random.insideUnitCircle * GetSpawnRadius();
            Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);
            spawnPosition = player.position + randomPosition;
        }
        while (!NavMesh.SamplePosition(spawnPosition, out hit, 1f, NavMesh.AllAreas));

        return hit.position; 
    }

    private float GetSpawnRadius()
    {
        return Random.Range(15f, spawnRadius);
    }

    public void OnEnemyKill()
    {
        currentEnemyCount--;
    }

    public void OnCoinCollect()
    {
        currentCoinCount--;
    }
}
