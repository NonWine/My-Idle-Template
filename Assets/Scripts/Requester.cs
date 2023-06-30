using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Requester : MonoBehaviour
{
    [SerializeField] private GameObject[] _icons;
    private Customer customer;


    private void Start()
    {
        customer = GetComponent<Customer>();
        SetIcon();
    }



    private void SetIcon()
    {
        VegetableType type = customer.GetItemType();

        switch (type)
        {
            case VegetableType.Tomato:
                _icons[0].SetActive(true);
                
                break;
            case VegetableType.Eeg:
                _icons[1].SetActive(true);
                break;
            case VegetableType.Tykva:
                _icons[2].SetActive(true);
                 
                break;
            default:
                break;
        }
    }
}
