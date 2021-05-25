using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem sparksParticles;
    
    private List<string> textToDisplay = new List<string>();
    private float timeToNextText;
    private int currentTextIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        timeToNextText = 0.0f;
        currentTextIndex = 0;

        textToDisplay.Add("Congratulation");
        textToDisplay.Add("All Errors Fixed");

        text.text = textToDisplay[0];
        
        sparksParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextText += Time.deltaTime;

        if (timeToNextText > 1.5f)
        {
            timeToNextText = 0.0f;
            
            currentTextIndex++;
            if (currentTextIndex >= textToDisplay.Count)
            {
                currentTextIndex = 0;
            }

            text.text = textToDisplay[currentTextIndex];
        }
    }
}