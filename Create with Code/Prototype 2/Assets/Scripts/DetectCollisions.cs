using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // When collision happens, destroy both projectile and animal game object and log message in console
        Debug.Log("You have successfully feeded that animal!");
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
