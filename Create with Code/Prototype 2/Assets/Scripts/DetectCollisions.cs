using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // When collision happens, destroy both projectile and animal game object
        // If animal hits player, destroy it and log "Game Over!" message

        if (other.CompareTag("Player"))
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Animal") || other.CompareTag("Food"))
        {
            gameManager.AddScore(5);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
