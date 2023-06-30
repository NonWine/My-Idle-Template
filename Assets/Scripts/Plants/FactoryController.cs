using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FactoryController : MonoBehaviour, IFactory
{
    [SerializeField] private VegetableType vegetableType;
    [Header("UI params")]
    [SerializeField] private TMP_Text text;
    private Factory factory;

    private void Awake()
    {
        factory = GetComponent<Factory>();
    }

    private void Start()
    {
       
      //  text.text = factory.storageList.Count.ToString() + "|" + factory.GetMaxSize().ToString();
    }

    public void AddinStorageList(Vegetable obj)
    {
        obj.transform.SetParent(factory.GetItemStoragePoint());
        obj.ThrowIT(factory.GetItemStoragePoint());
        factory.storageList.Add(obj);
        text.text = factory.storageList.Count.ToString() + "|" + factory.GetMaxSize().ToString();
        if(factory.storageList.Count == 1)
        factory.SpawnRx();
    }

    public void PlayerGiveItems(Human player)
    {
        for (int i = 0; i < player.items.Count; i++)
        {
            if (player.items[i].GetVegetableType() == vegetableType)
            {
                Vegetable veg = player.DeUpdateStorage(i);
                AddinStorageList(veg);
                break;
            }

        }
    }

    public void DeleteController()
    {
        Vegetable storageVag = factory.storageList[0];
        Destroy(storageVag.gameObject);
        factory.storageList.RemoveAt(0);
        text.text = factory.storageList.Count.ToString() + "|" + factory.GetMaxSize().ToString();
    }

    
}
