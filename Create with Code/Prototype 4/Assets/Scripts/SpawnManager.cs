using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerupPrefabs;
    public int enemyCount;
    public int waveNumber = 1;

    private float spawnRange = 9f;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePowerup();
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;

            SpawnEnemyWave(waveNumber);
            GeneratePowerup();
        }
    }

    private void GeneratePowerup()
    {
        var powerupRandomIndex = Random.Range(0, powerupPrefabs.Length);

        Instantiate(
            powerupPrefabs[powerupRandomIndex],
            GenerateSpawnPosition(),
            powerupPrefabs[powerupRandomIndex].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        var enemyRandomIndex = Random.Range(0, enemies.Length);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(
                enemies[enemyRandomIndex],
                GenerateSpawnPosition(),
                enemies[enemyRandomIndex].transform.rotation);
        }
    }
}
