using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private GameObject cardBoardBox;
    [SerializeField] private Transform boxPoint;
    [SerializeField] private Transform[] dollarPoints;
    [SerializeField] private GameObject dollar;
    private Queue<Customer> customers = new Queue<Customer>();
    private int dolarCount;
    private int currentQueryCount;
    [SerializeField] private Transform[] QueryPoints;
    
    public CardBoardBox CreateBox()
    {
      GameObject box =   Instantiate(cardBoardBox, boxPoint.position, Quaternion.identity);
        return box.GetComponent<CardBoardBox>();
    }

    public bool isEmpty()
    {
        if (customers.Count > 0)
            return false;
        else
            return true;
    }

    public void AddInQueue(Customer customer)
    {
        customers.Enqueue(customer);
    }

    public Customer GetCustomer() {  return customers.Dequeue(); }

    public void AddDolarCount() => dolarCount++;

    public void GiveDollars()
    {
        int k = dolarCount;
        for (int i = 0; i < k; i++)
        {
            if (i == dollarPoints.Length)
                i = 0;
            Instantiate(dollar, dollarPoints[i].position, Quaternion.Euler(-90f,0f,0f));
            dolarCount--;
        }
    }

    public void GetCashQuery(Customer customer)
    {
        customer.AgentCustomer.SetDestination(QueryPoints[currentQueryCount].position);
        currentQueryCount++;
    }

    public void DecreaseCurrentQuery() => currentQueryCount--;
}
