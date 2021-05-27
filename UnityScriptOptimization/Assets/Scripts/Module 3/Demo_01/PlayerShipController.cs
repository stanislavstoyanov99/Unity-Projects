using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    private ObjectPool objectPool;
    
    private Transform myTransform;
  
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        objectPool = GetComponent<ObjectPool>();

        InvokeRepeating("Shoot", .33f, .33f);
    }

    void Shoot()
    {
        GameObject bullet = objectPool.GetAvailableObject();
        bullet.transform.position = myTransform.position;
        bullet.SetActive(true);
    }
}
