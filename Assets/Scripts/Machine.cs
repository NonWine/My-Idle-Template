using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Machine : MonoBehaviour
{
    [SerializeField] private Vegetable vegetable;
    [SerializeField] private Transform[] spawnPointVegetable;
    [SerializeField] private VegetableStorage[] vegetableStorages;
    [SerializeField] private float timeCreate;
    private CompositeDisposable disposable = new CompositeDisposable();
    private List<Vegetable> vegetables = new List<Vegetable>();
    private float timer, timerSpawn, giveTimerDelay;
    private int currentVeg;
    
    private void Start()
    {
        for(int i=0; i < vegetableStorages.Length; i++)
        {
            vegetableStorages[i].storageList = new List<Vegetable>();
        }
    }


    public  void GiveItems(Player player)
    {
        ////player GetItems
        if (!player.isMaxStorage() && currentVeg > 0)
        {
            giveTimerDelay += Time.deltaTime;
            if(giveTimerDelay >= 0.5f)
            {
                giveTimerDelay = 0f;
                Vegetable vegetable = vegetables[0];
                vegetable.transform.SetParent(player.transform);
                vegetable.ThrowIT(player.GetPoint(), 6);
                player.UpdateStorage(vegetable);
                vegetables.RemoveAt(0);
                currentVeg--;
            }
        }

        //player Give Items
        else if (!player.isEmptyStorage())
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0f;
                CheckPlayer(player);
                if (CheckRequestIngredients())
                {
                    DestroyIngredients();
                }
                   

            }
        }


    }

    private void CheckPlayer(Player player)
    {
        for (int i = 0; i < player.items.Count; i++)
        {
            for (int j = 0; j < vegetableStorages.Length; j++)
            {
               if(player.items[i].GetVegetableType() == vegetableStorages[j].vegetableType)
                {
                    if(vegetableStorages[j].storageList.Count < vegetableStorages[j].maxStorage)
                    {
                        Vegetable obj = player.DeUpdateStorage(i);
                        obj.transform.SetParent(vegetableStorages[j].itemStoragePoints[0]);
                        obj.ThrowIT(vegetableStorages[j].itemStoragePoints[0]);
                        vegetableStorages[j].storageList.Add(obj);
                        vegetableStorages[j].text.text = vegetableStorages[j].storageList.Count.ToString() + "|" + vegetableStorages[j].maxStorage.ToString();
                        break;
                    }
                   
                }
            }
        }
    
    }

    private bool CheckRequestIngredients()
    {
        for (int i = 0; i < vegetableStorages.Length; i++)
        {
            if (vegetableStorages[i].storageList.Count < vegetableStorages[i].requestCount)
                return false;
        }
        return true;
    }

    private void DestroyIngredients()
    {
        for (int i = 0; i < vegetableStorages.Length; i++)
        {
            for (int j = 0; j < vegetableStorages[i].requestCount; j++)
            {
                Vegetable veg = vegetableStorages[i].storageList[0];
                Destroy(veg.gameObject);
                vegetableStorages[i].storageList.RemoveAt(0);


            }
            vegetableStorages[i].text.text = vegetableStorages[i].storageList.Count.ToString() + "|" + vegetableStorages[i].maxStorage.ToString();
        }
        Observable.EveryUpdate().Subscribe(_ => { CreateItem(); }).AddTo(disposable);
    }

    private void CreateItem()
    {
        timerSpawn += Time.deltaTime;
        if(timerSpawn >= timeCreate)
        {
           Vegetable veg = Instantiate(vegetable, spawnPointVegetable[currentVeg].position, Quaternion.identity);
            vegetables.Add(veg);
            currentVeg++;
            timerSpawn = 0f;
            disposable.Clear();
            if (CheckRequestIngredients())
            {
                DestroyIngredients();
            }
        }
    }
}

[System.Serializable]
public struct VegetableStorage
{
    public Transform[] itemStoragePoints;
 //   [System.NonSerialized]
    public List<Vegetable> storageList;
    public VegetableType vegetableType;
    public int requestCount;
    public int maxStorage;
    public TMPro.TMP_Text text;

}
