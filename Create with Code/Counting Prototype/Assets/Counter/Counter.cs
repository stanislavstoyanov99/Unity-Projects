using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int counter;

    private void Start()
    {
        counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        counter++;
        CounterText.text = $"Count : {counter}";
    }
}
