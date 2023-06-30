using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegiterState : State
{
    private Customer customer;
    private CashRegister cashRegister;
    private bool incash;
    private CardBoardBox box;
    private float timer;
    private Transform homePoint;
    private float timeDelay = 0.1f;
    public CashRegiterState(Customer customer, CashRegister cashRegister)
    {
        this.customer = customer;
        this.cashRegister = cashRegister;
    }

    public override void Enter()
    {
        customer.AnimCustomer.SetInteger("state", 2);
        customer.AgentCustomer.velocity = Vector3.zero;
        customer.AgentCustomer.isStopped = true;
        cashRegister.AddInQueue(customer);
        homePoint = customer.GetHomePoint();
    }

    public override void Update()
    {
        if (incash)
        {
            timer += Time.deltaTime;
            if(timer >= timeDelay)
            {
                if (customer.GetItems().Count > 0)
                {
                    Vegetable item = customer.items[0];
                    item.gameObject.transform.localScale = new Vector3(0.849921286f, 0.849921286f, 0.849921286f);
                    item.transform.SetParent(box.transform);
                    item.ThrowIT(box.GetPoint(customer.GetItems().Count));
                   customer.DeUpdateStorage(0);
                    cashRegister.AddDolarCount();
                    timer = 0f;
                }

                if (customer.GetItems().Count == 0  && timer >= 0.25f)
                {
                    box.transform.position = Vector3.Lerp(box.GetStartPos(), customer.GetBoPlace().transform.position, 2.5f * timer);
                    if (box.transform.position == customer.GetBoPlace().transform.position)
                    {
                        incash = false;
                        customer.SetTrig(false);
                        timer = 0f;
                        box.transform.SetParent(customer.transform);
                        customer.AgentCustomer.isStopped = false;
                        customer.AgentCustomer.SetDestination(homePoint.position);
                        customer.AnimCustomer.SetInteger("state", 3);
                        customer.GetCustomerStatmeMachine().ChangeState(new IdleState());
                        cashRegister.GiveDollars();
                        cashRegister.DecreaseCurrentQuery();
                        CustomerSpawner.Instance.ClearFromWait();
                    }

                }
            }
           
        }
   
    }

    public void BuyItems(CardBoardBox cardBoardBox)
    {
        box = cardBoardBox;
        incash = true;
        timer = -0.5f;
    }
}
