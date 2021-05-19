using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
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

            GeneratePowerup();
            SpawnEnemyWave(waveNumber);
        }
    }

    private void GeneratePowerup()
    {
        Instantiate(
            powerupPrefab,
            GenerateSpawnPosition(),
            powerupPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(
                enemyPrefab,
                GenerateSpawnPosition(),
                enemyPrefab.transform.rotation);
        }
    }
}
