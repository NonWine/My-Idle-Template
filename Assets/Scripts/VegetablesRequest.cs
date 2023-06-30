using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "VegetablesRequest", order = 1)]
public class VegetablesRequest : ScriptableObject
{
    [SerializeField] private VegetableType[] types;
    [SerializeField] private int[] counts;
    private TMP_Text[] texts;
    private int[] currentCouns = new int[10];

    public void SetTexts(TMP_Text[] texts) => this.texts = texts;

    public void ClearCurrentCounts()
    {
        for (int i = 0; i < currentCouns.Length; i++)
        {
            currentCouns[i] = 0;
        }
    }

    public bool CheckType(VegetableType vegetableType)
    {
        for (int i = 0; i < types.Length; i++)
        {
            if (vegetableType == types[i] && counts[i] > currentCouns[i])
                return true;
        }


        return false;
    }

    public void SetInReceipt(Vegetable veg)
    {
        VegetableType type = veg.GetVegetableType();
        for (int i = 0; i < types.Length; i++)
        {
            if (type == types[i])
            {
                currentCouns[i]++;
                texts[i].text = currentCouns[i].ToString() + "|" + counts[i].ToString();
                break;
            }
        }
    }

    public bool CheckReceipt()
    {
        for (int i = 0; i < counts.Length; i++)
        {
            if (currentCouns[i] < counts[i])
                return false;
        }
        for (int i = 0; i < counts.Length; i++)
        {
            currentCouns[i] = 0;
          
        }
        return true;
    }

    public int GetRequestCount()
    {
        int x = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            x += counts[i];
            currentCouns[i] -= counts[i];
        }
        return x;
    }

    public void ResetText()
    {
        for (int i = 0; i < counts.Length; i++)
        {
          
            texts[i].text = currentCouns[i].ToString() + "|" + counts[i].ToString();
        }
    }
}
