using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Plant
{

    [SerializeField] private Transform[] StoragePointsIngredients;
    private int maxSizeIngredients;
    private IFactory factoryController;
    public List<Vegetable> storageList { get; set; }

    private void Awake()
    {
        factoryController = GetComponent<IFactory>();
    }

    protected override void Start()
    {
        base.Start();
        maxSizeIngredients = StoragePointsIngredients.Length;
        
        storageList = new List<Vegetable>(5);
        CustomerSpawner.Instance.SetNewVegType(vegetable.GetVegetableType());
    }


    public override void Spawn()
    {

        timer2 += Time.deltaTime;
        if (timer2 >= timerSpawn)
        {
            factoryController.DeleteController();
            Vegetable veg = Instantiate(vegetable, spawnItemsPoints[itemList.Count].position, Quaternion.identity);
            itemList.Push(veg);
            timer2 = 0f;
            if (storageList.Count == 0)
                disposable.Clear();
            else
                SpawnRx();
        }




    }

    public override void GiveItems(Human player)
    {
        //player GetItems
        if (isHaveItem() && !player.isMaxStorage()) // !CheckPlayer(player)
        {
            giveItemsTimer += Time.deltaTime;
            if (giveItemsTimer >= 0.1f && CheckVeg().isActive)
            {
                giveItemsTimer = 0f;
                Vegetable veg = GetVegetable();
                veg.transform.SetParent(player.transform);
                veg.ThrowIT(player.GetPoint(), 6);
                player.UpdateStorage(veg);

            }
        }
        //player Give Items
        if (!player.isEmptyStorage() && !IsStorageMax())
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0f;
                factoryController.PlayerGiveItems(player);
               
            }
        }
       
        
    }

    public Transform GetItemStoragePoint()
    {
        return StoragePointsIngredients[storageList.Count];
    }

    public bool IsStorageMax()
    {
        if (storageList.Count == maxSizeIngredients)
            return true;
        else
            return false;
    }

    public int GetMaxSize() { return maxSizeIngredients; }


    public void SetNewTimerSpawn(float value) => timerSpawn = value;

    public float GetTimerSpawn() { return timerSpawn; }
}
