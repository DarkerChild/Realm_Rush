using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    [SerializeField] float cycleTime = 1f;
    [Range(0.1f, 5f)] float movementFactor;

    CanvasGroup canvasGroup;

    const float tau = Mathf.PI * 2; //About 6.28

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        float cycles = Time.time / cycleTime;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave / 2) + 0.5f;

        canvasGroup.alpha = movementFactor;
    }
}
