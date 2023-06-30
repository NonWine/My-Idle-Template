using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeData : MonoBehaviour
{
    [SerializeField] private int[] costs;
    [SerializeField] private string nameLevel;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] private TMP_Text costText;
    private int currentIndex;

    private void Start()
    {
        textLevel.text = nameLevel + "- lvl" + (currentIndex + 1).ToString();
        costText.text = costs[currentIndex].ToString();
    }


    public void ChangeIndex()
    {
        currentIndex++;
        if(currentIndex == costs.Length)
        {
            textLevel.text = nameLevel + "- lvl MAX";
            costText.text = "MAX";
            GetComponent<UnityEngine.UI.Button>().interactable = false;
            return;
        }
        textLevel.text = nameLevel + "- lvl" + (currentIndex + 1).ToString();
        costText.text = costs[currentIndex].ToString();
    }

    public int GetCurrentPrice() { return costs[currentIndex]; }
}
