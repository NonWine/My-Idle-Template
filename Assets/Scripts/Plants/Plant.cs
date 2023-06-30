using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Plant : MonoBehaviour
{
    [SerializeField] protected PlantType plantType;
    [SerializeField] protected Vegetable vegetable;
    [SerializeField] protected Transform[] spawnItemsPoints;
    [SerializeField] protected float timerSpawn;
    protected Stack<Vegetable> itemList = new Stack<Vegetable>(5);
    protected CompositeDisposable disposable = new CompositeDisposable();
    protected int count;
    protected float timer,giveItemsTimer, timer2;
    protected int maxSizeItems;

    protected virtual void Start()
    {
        Invoke(nameof(TurnOnCollider), 0.5f);
        maxSizeItems = spawnItemsPoints.Length;
        ItemsManager.Instance.Plants.Add(this);
    }

    public void SetTimeSpawn()
    {

    }

    public virtual void Spawn()
    {

    }

    public virtual void GiveItems(Human player)
    {
        
    }

    public void SpawnRx()
    {
        Observable.EveryUpdate().Subscribe(_ => { Spawn(); }).AddTo(disposable);
    }

    public void StopSpawnRx() => disposable.Clear();

    public Vegetable CheckVeg() { return itemList.Peek(); }

    public Vegetable GetVegetable()
    {
        count--;
        return itemList.Pop();

    }

    public bool isHaveItem()
    {
        if (itemList.Count == 0)
            return false;
        else
            return true;
    }

    public void TurnOnCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    public bool isItemsMax()
    {
        if (itemList.Count == maxSizeItems)
            return true;
        else
            return false;
    }

    public PlantType GetPlantType() { return plantType; }
    
}
public enum PlantType {Plant,Factory }