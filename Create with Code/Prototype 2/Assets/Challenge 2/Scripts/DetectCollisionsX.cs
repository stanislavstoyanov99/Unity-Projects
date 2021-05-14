using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Congratulations! The ball was collected by your dog!");
        Destroy(gameObject);
    }
}
