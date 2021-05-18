using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private PlayerController playerControllerScript;
    private float startDelay = 2f;
    private float repeatRate = 2f;
    private Vector3 spawnPos = new Vector3(30, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        var obstacleIndex = Random.Range(0, obstaclePrefabs.Length);

        if (playerControllerScript.gameOver == false)
        {
            Instantiate(
                obstaclePrefabs[obstacleIndex],
                spawnPos,
                obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
}
