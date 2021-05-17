using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    
    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 2f;
    }
    
    void Update()
    {
        var randomRedColor = Random.Range(0.3f, 0.9f);
        var randomGreenColor = Random.Range(0.2f, 1.0f);
        var randomBlueColor = Random.Range(0.1f, 0.3f);

        Material material = Renderer.material;
        material.color = new Color(randomRedColor, randomGreenColor, randomBlueColor, 0.4f);

        transform.Rotate(30.0f * Time.deltaTime, 0.0f, 0.0f);
    }
}
