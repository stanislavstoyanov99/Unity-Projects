using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    public float sideSpawnMinZ;
    public float sideSpawnMaxZ;
    public float sideSpawnX;

    private float spawnRangeX = 20f;
    private float spawnRangeZ = 20f;

    private float startDelay = 2f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
        InvokeRepeating(nameof(SpawnLeftAnimal), startDelay + 1, spawnInterval + 1);
        InvokeRepeating(nameof(SpawnRightAnimal), startDelay + 1, spawnInterval + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        var animalIndex = Random.Range(0, animalPrefabs.Length);

        var spawnVerticalPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);

        Instantiate(
            animalPrefabs[animalIndex],
            spawnVerticalPos,
            animalPrefabs[animalIndex].transform.rotation);
    }

    void SpawnLeftAnimal()
    {
        var animalIndex = Random.Range(0, animalPrefabs.Length);

        var spawnPos = new Vector3(-sideSpawnX, 0, Random.Range(sideSpawnMinZ, sideSpawnMaxZ));

        var rotation = new Vector3(0, 90, 0);

        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            Quaternion.Euler(rotation));
    }

    void SpawnRightAnimal()
    {
        var animalIndex = Random.Range(0, animalPrefabs.Length);

        var spawnPos = new Vector3(sideSpawnX, 0, Random.Range(sideSpawnMinZ, sideSpawnMaxZ));

        var rotation = new Vector3(0, -90, 0);

        Instantiate(
            animalPrefabs[animalIndex],
            spawnPos,
            Quaternion.Euler(rotation));
    }
}
