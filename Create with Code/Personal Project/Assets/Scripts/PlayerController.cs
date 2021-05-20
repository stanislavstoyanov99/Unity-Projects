using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 100f;
    private float zTopBound = 6f;
    private float zBottomBound = -7.5f;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    // Moves the player based on arrow key input
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    // Prevents the player from leaving the top of bottom of the screen
    private void ConstrainPlayerPosition()
    {
        if (transform.position.z < zBottomBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBottomBound);
        }

        if (transform.position.z > zTopBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zTopBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with enemy.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
