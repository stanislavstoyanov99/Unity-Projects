using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    private float yRange = -10f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yRange)
        {
            Destroy(gameObject);
        }

        var lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }
}
