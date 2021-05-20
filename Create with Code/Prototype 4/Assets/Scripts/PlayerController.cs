using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public PowerUpType currentPowerUp = PowerUpType.None;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    private float powerupStrength = 15.0f;
    private bool smashing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        var forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerUp == PowerUpType.Smash &&
            Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            currentPowerUp == PowerUpType.Pushback)
        {
            var enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            var enemyPosition = collision.gameObject.transform.position;
            var playerPosition = transform.position;

            var awayFromPlayer = enemyPosition - playerPosition;

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log($"Collided with {collision.gameObject.name} with powerup set to {currentPowerUp.ToString()}");
        }
    }

    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(
                rocketPrefab,
                transform.position + Vector3.up,
                Quaternion.identity);

            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        // Store the y position before taking off
        var floorY = transform.position.y;

        // Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            // moves the player up while still keeping their x velocity
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        // Now move the player down
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        // Cycle through all enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from our position.
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>()
                    .AddExplosionForce(
                        explosionForce,
                        transform.position,
                        explosionRadius,
                        0.0f,
                        ForceMode.Impulse);
            }
        }

        smashing = false;
    }
}
