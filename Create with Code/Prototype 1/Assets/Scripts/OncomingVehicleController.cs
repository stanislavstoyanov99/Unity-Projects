using UnityEngine;

public class OncomingVehicleController : MonoBehaviour
{
    private float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
