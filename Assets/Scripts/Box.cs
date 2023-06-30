using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Box : MonoBehaviour
{
    [SerializeField] private VegetableType vegetableType;
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Transform[] customerPoints;
    [SerializeField] private TMP_Text text;
    private Stack<Vegetable> vegetables = new Stack<Vegetable>();
    private Queue<Customer> customers = new Queue<Customer>();
    private int maxCount;
    private int currentCount;

    private void Start()
    {
        maxCount = points.Length;
        ItemsManager.Instance.Boxes.Add(this);
        text.text  = currentCount.ToString() + "|" + maxCount;

    }

    public Transform GetThrowPoint() { return throwPoint; }

    private void Update()
    {
        if(customers.Count > 0 && vegetables.Count > 0)
        {
           Vegetable vegetable =  vegetables.Peek();
            if (!vegetable.GetCanMove())
            {
                Customer customer = customers.Peek();
                customer.GetBoxState().GetItemFromBox();
            }
        }
    }

    public void GetItem(Vegetable veg)
    {
        if(currentCount < maxCount && vegetableType == veg.GetVegetableType())
        {
          
            vegetables.Push(veg);
            veg.transform.SetParent(transform);
            veg.ThrowIT(points[currentCount]);
            currentCount++;
            text.text = currentCount.ToString() + "|" + maxCount;
        }
      
    }

    public bool FullBox()
    {
        if (currentCount >= maxCount)
            return true;
        else
            return false;
    }

    public Vegetable GiveItem(Transform point)
    {
        if(currentCount > 0)
        {
            Vegetable item = vegetables.Pop();
            currentCount--;
            item.ThrowIT(point);
            text.text = currentCount.ToString() + "|" + maxCount;
            return item;
        }
        return null;
    }

    public bool isEmptyBox() 
    {
        if(currentCount > 0)
        {
            return false;
 
        }
        else
           return true;
    }

    public void AddToQueue(Customer customer)
    {
        customers.Enqueue(customer);
    }

    public void LeftFromQueue() => customers.Dequeue();

    public Transform GetCurrentPoint() { return points[currentCount]; }

    public Transform GetCustomersPoints()
    {
        return customerPoints[customers.Count];
    }

    public VegetableType GetBoxType() { return vegetableType; }
}