using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject workersPanel;
    [SerializeField] private GameObject machinesPanel;
    [SerializeField] private Human[] humen;
    [SerializeField] private AdvancedFactoryController[] factories;
 

    public void CarryUpSpeed(UpgradeData upgradeData)
    {
        if (Bank.Instance.CoinsCount >= upgradeData.GetCurrentPrice())
        {
            humen[1].SpeedUpgrade(1);
            upgradeData.ChangeIndex();
        }

    }

    public void PlayerStackUp(UpgradeData upgradeData)
    {
        if (Bank.Instance.CoinsCount >= upgradeData.GetCurrentPrice())
        {
            humen[0].IncreaseStack();
            upgradeData.ChangeIndex();
        }


    }

    public void CarryStackUp(UpgradeData upgradeData)
    {
        if (Bank.Instance.CoinsCount >= upgradeData.GetCurrentPrice())
        {
            humen[1].IncreaseStack();
            upgradeData.ChangeIndex();
        }


    }

    public void FactoryUp(UpgradeData upgradeData)
    {
        if (Bank.Instance.CoinsCount >= upgradeData.GetCurrentPrice())
        {
            factories[0].SpeedUP(0.5f);
            upgradeData.ChangeIndex();
        }
    }
    public void TurnOnUpgradePanel()
    {
        upgradePanel.SetActive(true);
        upgradePanel.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, 0f, 0f), 0.2f)
            .SetUpdate(UpdateType.Normal, true);
        Time.timeScale = 0f;
        DOTween.To(() => Vector3.zero, x => upgradePanel.transform.localScale = x, new Vector3(1f, 1f, 1f), 1).
            SetUpdate(UpdateType.Normal, true);
           
        
    }

    public void TurnOfUpgradePanel()
    {
        Time.timeScale = 1f;
        DOTween.Sequence()
        .Append(upgradePanel.transform.DOScale(new Vector3(0.3f,0.3f,0.3f), 0.5f))
        .Append(upgradePanel.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, 1000f, 0f), 0.2f))
        .OnComplete(() => upgradePanel.SetActive(false));
    }

    public void ShowWorkersPanel(bool flag) => workersPanel.SetActive(flag);

    public void ShowMachinesPanel(bool flag) => machinesPanel.SetActive(flag);




}
