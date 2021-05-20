using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerup;

    private float zEnemySpawn = 12.0f;
    private float ySpawn = 0.75f;
    private float xSpawnRange = 12.0f;
    private float zPowerupRange = 5.0f;

    private float powerupSpawnTime = 5.0f;
    private float enemySpawnTime = 1.0f;
    private float starDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), starDelay, enemySpawnTime);
        InvokeRepeating(nameof(SpawnPowerup), starDelay, powerupSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        var randomX = Random.Range(-xSpawnRange, xSpawnRange);
        var randomIndex = Random.Range(0, enemies.Length);

        var spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(
            enemies[randomIndex],
            spawnPos,
            enemies[randomIndex].gameObject.transform.rotation);
    }

    private void SpawnPowerup()
    {
        var randomX = Random.Range(-xSpawnRange, xSpawnRange);
        var randomZ = Random.Range(-zPowerupRange, zPowerupRange);

        var spawnPos = new Vector3(randomX, ySpawn, randomZ);

        Instantiate(
            powerup,
            spawnPos,
            powerup.transform.rotation);
    }
}
