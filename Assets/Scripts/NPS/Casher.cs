using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Casher : MonoBehaviour
{
   [SerializeField] private CashRegister cashRegister;
    private CompositeDisposable disposable = new CompositeDisposable();
    private float timer;
    private float timeDelay = 1f;
    private List<Customer> customers = new List<Customer>();



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            customers.Add(other.GetComponent<Customer>());
            if(!disposable.IsDisposed)
            Observable.EveryUpdate().Subscribe(_ => { SearchClient(); }).AddTo(disposable);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {
            customers.Remove(other.GetComponent<Customer>());
            if(customers.Count == 0)
                disposable.Clear();
        }
    }

    private void SearchClient()
    {
        if (!cashRegister.isEmpty())
        {
            timer += Time.deltaTime;
            if (timer >= timeDelay)
            {
                CardBoardBox cardBoardBox = cashRegister.CreateBox();
                cashRegister.GetCustomer().GetCashState().BuyItems(cardBoardBox);
                timer = 0f;
                
            }


        }
    }

}
