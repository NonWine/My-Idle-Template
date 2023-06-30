using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxState : State
{
    private Box currentBox;
    private Customer customer;
    private bool canTake;
    private float timer;
    private float timeDelay = 0.1f;
    public BoxState(Customer customer, Box box)
    {
        this.customer = customer;
        this.currentBox = box;
    }

    public override void Enter()
    {
        customer.AgentCustomer.velocity = Vector3.zero;
        customer.AgentCustomer.isStopped = true;

            customer.AnimCustomer.SetInteger("state", 0);
            currentBox.AddToQueue(customer);
        Debug.Log("Add");
        if(!currentBox.isEmptyBox())
        {
            customer.AnimCustomer.SetInteger("state", 2);
            canTake = true;
        }
    }

    public override void Exit()
    {
        canTake = false;
        timer = 0f;
        currentBox.LeftFromQueue();
        Debug.Log("left");
        customer.AgentCustomer.isStopped = false;
        customer.CusstomerCashRegister.GetCashQuery(customer);
        customer.AnimCustomer.SetInteger("state", 3);
        customer.SetTrig(false);

    }

    public override void Update()
    {
        if (canTake && !currentBox.isEmptyBox())
        {
            timer += Time.deltaTime;
            if(timer >= timeDelay)
            {
                Vegetable veg = currentBox.GiveItem(customer.GetPoint());
                veg.transform.SetParent(customer.transform);
                customer.UpdateStorage(veg);
                if (currentBox.isEmptyBox())
                    timer = -1f;
                else
                    timer = 0f;   
            }
            if (customer.isMaxStorage())
            {
                Exit();
            }
        }
    }

    public void GetItemFromBox()
    {
        canTake = true;
        customer.AnimCustomer.SetInteger("state", 2);
    }
}
