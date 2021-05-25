using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    [SerializeField] private float horsePower = 0f;
    [SerializeField] float speed;
    [SerializeField] private const float turnSpeed = 25f;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] GameObject centerOfMass;

    private Rigidbody playerRb;
    private float horizontalInput;
    private float forwardInput;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f); // 2.237 for mph;
        speedometerText.SetText($"Speed: {speed} kph");

        // Gets player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Moves the car forward based on the vertical input
        playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);

        // Rotates the car based on the horizontal input
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);

        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
