using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AdvancedFactoryController : MonoBehaviour, IFactory
{
    [SerializeField] private VegetablesRequest vegetablesRequest;
    [Header("UI Params")]
    [SerializeField] private TMP_Text[] texts;
    private Factory factory;

    private void Start()
    {
        factory = GetComponent<Factory>();
        vegetablesRequest.ClearCurrentCounts();
        vegetablesRequest.SetTexts(texts);
    }

    public void AddinStorageList(Vegetable obj)
    {
        obj.transform.SetParent(factory.GetItemStoragePoint());
        obj.ThrowIT(factory.GetItemStoragePoint());
        factory.storageList.Add(obj);
        vegetablesRequest.SetInReceipt(obj);
        if (vegetablesRequest.CheckReceipt())
        {
         
            factory.SpawnRx();
        }
       
    }

    public void PlayerGiveItems(Human player)
    {
        for (int i = 0; i < player.items.Count; i++)
        {
            if (vegetablesRequest.CheckType(player.items[i].GetVegetableType()))
            {
                Vegetable veg = player.DeUpdateStorage(i);
                AddinStorageList(veg);
                break;
            }

        }
    }

    public void DeleteController()
    {

        for (int i = 0; i < factory.storageList.Count; i++)
        {
           
            Destroy(factory.storageList[i].gameObject);
            
        }
        factory.storageList.Clear();
        factory.storageList = new List<Vegetable>(5);
        vegetablesRequest.ResetText();
    }

    public void SpeedUP(float value)
    {
        factory.SetNewTimerSpawn(factory.GetTimerSpawn() - value);
    }
}
