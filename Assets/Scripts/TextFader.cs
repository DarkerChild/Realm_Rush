using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    [SerializeField] float cycleTime = 2f;
    [Range(0.1f, 5f)] float movementFactor;

    Text startText;

    const float tau = Mathf.PI * 2; //About 6.28

    private void Start()
    {
        startText = GameObject.Find("Press Space").GetComponent<Text>();
    }

    private void Update()
    {
        float cycles = Time.time / cycleTime;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave / 2) + 0.5f;

        Color newColor = startText.color;
        newColor.a = movementFactor;

        startText.material.color = newColor;

    }
}
