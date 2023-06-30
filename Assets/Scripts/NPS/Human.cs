using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] protected int maxStorage;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform[] itemPoints;
    protected StateMachine stateMachine;
    public List<Vegetable> items { get; set; }
    public int currentStorage;

    
    protected virtual void Start()
    {
       
        items = new List<Vegetable>(maxStorage);
        stateMachine = new StateMachine();
        stateMachine.Initialize(new IdleState());
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }


    public Transform GetPoint() { return itemPoints[currentStorage]; }

    public virtual void UpdateStorage(Vegetable item)
    {
        items.Add(item);
        currentStorage++;
    }

    public virtual void ThrowToBox(Box box)
    {

    }

    public virtual Vegetable DeUpdateStorage(int i)
    {
        
        if (!isEmptyStorage())
        {
            Vegetable veg = items[i];
            items.RemoveAt(i);
            currentStorage--;
         
            return veg;
        }
        else
            return null;
    }

    public bool isMaxStorage()
    {
        if (currentStorage == maxStorage)
            return true;
        else
            return false;

    }

    public bool isEmptyStorage()
    {
        if (currentStorage == 0)
            return true;
        else
            return false;

    }

    public virtual void SetIdleState() => stateMachine.ChangeState(new IdleState());

    public StateMachine GetStateMachine() { return stateMachine; }

    public List<Vegetable> GetItems() { return items; }

    public void ReCollectList()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = itemPoints[i].position;
        }

    }

    public virtual void IncreaseStack()
    {
        maxStorage++;
    }

    public virtual void SpeedUpgrade(float value)
    {

    }
}
