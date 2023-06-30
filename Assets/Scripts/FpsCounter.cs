using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    private int lastFrameIndex;
    private float[] framDeltaTimeArray;
    
    private void Awake()
    {
        framDeltaTimeArray = new float[50];
    }

    // Update is called once per frame
    private void Update()
    {
        framDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % framDeltaTimeArray.Length;
        fpsText.text = Mathf.RoundToInt(CalculateFps()).ToString();
    }
    
    private float CalculateFps()
    {
        float total = 0f;
        foreach (var deltaTime in framDeltaTimeArray)
        {
            total += deltaTime;
        }
        return framDeltaTimeArray.Length / total;
    }
}
