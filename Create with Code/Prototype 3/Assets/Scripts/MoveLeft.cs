using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30f;

    private float leftBound = -10;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            CheckForDoubleSpeed();
        }

        DestroyObstacleAfterLeavingScreen();
    }

    void CheckForDoubleSpeed()
    {
        if (playerControllerScript.doubleSpeed)
        {
            transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    void DestroyObstacleAfterLeavingScreen()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
