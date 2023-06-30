using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
public class Carry : Human
{
    private NavMeshAgent agent;
    
    public CompositeDisposable Disposables { get; private set; }

    public NavMeshAgent AgentCarry { get; private set; }

    public Animator AnimatorCarry { get; private set; }

    protected override void Start()
    {
        Disposables = new CompositeDisposable();
        items = new List<Vegetable>(maxStorage);
        stateMachine = new StateMachine();
        agent = GetComponent<NavMeshAgent>();
        AgentCarry = agent;
        stateMachine.Initialize(new IdleCarryState(this));
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(stateMachine.currentState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            stateMachine.ChangeState(new PlayerPlantState(this, other.GetComponent<Plant>()));
            Debug.Log("carry enter to Plant");
        }
        else if (other.CompareTag("Box"))
        {
            stateMachine.ChangeState(new PlayerBoxState(this, other.GetComponent<Box>()));
        }
    }

    private bool CheckBoxWithItems(VegetableType boxType)
    {
        foreach (var item in items)
        {
            if (item.GetVegetableType() == boxType)
                return true;
        }
        return false;
    }


    public void Work()
    {
        if(isMaxStorage())
        {
            foreach (var item in ItemsManager.Instance.Boxes)
            {
                if (!item.FullBox())
                {
                    agent.SetDestination(item.transform.position);
                    animator.SetInteger("state", 3);
                    Disposables.Clear();
                }
            }
        }
        else if(!isMaxStorage() )
        {
            foreach (var item in ItemsManager.Instance.Plants)
            {
                if (item.isHaveItem())
                {
                    animator.SetInteger("state", 1);
                    agent.SetDestination(item.transform.position);
                    Disposables.Clear();
                }

            }
        }
      
    }

    public void StartEveryUpdate(System.Action action)
    {
        Observable.EveryUpdate().Subscribe(_ => { action(); }).AddTo(Disposables);
    }


    public override void UpdateStorage(Vegetable item)
    {
        base.UpdateStorage(item);
        animator.SetInteger("state",2);
        StartEveryUpdate(Work);
    }

    public override Vegetable DeUpdateStorage(int i)
    {
        if (!isEmptyStorage())
        {
            animator.SetInteger("state", 2);
            Vegetable veg = items[i];
            items.RemoveAt(i);
            currentStorage--;
            if (isEmptyStorage())
                StartEveryUpdate(Work);
            return veg;
        }
        else
        return null;
    }

    public override void ThrowToBox(Box box)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetVegetableType() == box.GetBoxType())
            {
                Vegetable veg = DeUpdateStorage(i);
                box.GetItem(veg);
                ReCollectList();
                break;
            }
            if (i == items.Count - 1)
            {
                foreach (var item in ItemsManager.Instance.Boxes)
                {
                    if (!item.FullBox() && CheckBoxWithItems(item.GetBoxType()))
                    {
                        agent.SetDestination(item.transform.position);
                        animator.SetInteger("state", 3);

                    }
                }
            }
        }
    }

    public override void SpeedUpgrade(float value)
    {
        agent.acceleration += value;
        agent.speed += value;
    }

    public override void SetIdleState() => stateMachine.ChangeState(new IdleCarryState(this));
}
