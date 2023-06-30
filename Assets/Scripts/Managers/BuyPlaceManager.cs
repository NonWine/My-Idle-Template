using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlaceManager : MonoBehaviour
{
    public static BuyPlaceManager Instance { get; private set; }
    [SerializeField] private Transform[] places;
    private int startIndex;

    private void Awake()
    {
        Instance = this;
        startIndex = PlayerPrefs.GetInt("startIndex", startIndex);
    }

    private void Start()
    {
        if(startIndex < places.Length)
        places[startIndex].gameObject.SetActive(true);
        LoadBuiedPlants();
    }

    private void LoadBuiedPlants()
    {
        for (int i = 0; i < startIndex; i++)
        {
            places[i].GetComponent<BuyZona>().TurnOnPlant();
            places[i].gameObject.SetActive(false);
        }
    }

    public void MoveIndex()
    {
        startIndex++;
        PlayerPrefs.SetInt("startIndex", startIndex);
        if(startIndex != places.Length)
        places[startIndex].gameObject.SetActive(true);
    }

    public void AddCustomerToCustomerManager()
    {
        CustomerSpawner.Instance.AddMaxCount();
    }


}
