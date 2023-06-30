using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
public class BuyZona : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private int cost;
    [SerializeField] private TMP_Text text;
    [SerializeField] private bool addClient;
    private float timer;
    private float timeDelay = 0.1f;

    private void Start()
    {
        text.text = cost.ToString();
    }

    public void BuyPlant()
    {
        if (Bank.Instance.CoinsCount > 0)
        {
            if(timer > timeDelay)
            {
                Bank.Instance.ReduceCoins(1);
                cost--;
                text.text = cost.ToString();
                if (cost == 0)
                {
                    SpawnPlant();
                }
                timer = 0f;
            }
            timer += Time.deltaTime;

        }
    }

    public void SpawnPlant()
    {
        plant.SetActive(true);
        if(plant.GetComponent<ScaleCreator>()!= null)
        plant.GetComponent<ScaleCreator>().StartScaleCreator();
        Player.Instance.ClearBuyDispose();
        BuyPlaceManager.Instance.MoveIndex();
        if (addClient)
            BuyPlaceManager.Instance.AddCustomerToCustomerManager();
        gameObject.SetActive(false);
    }

    public void TurnOnPlant() => plant.SetActive(true);

}
