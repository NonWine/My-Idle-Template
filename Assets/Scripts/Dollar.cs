using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    private Vector3 startPoint;
    private Transform endPoint;
    private bool isTake;
    private float timer;

    private void Start()
    {
        startPoint = transform.position;
        Invoke(nameof(TurnOnTriger), 1f);
    }

    private void Update()
    {
        if (isTake)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, endPoint.position, 4 * timer);
            if(transform.position == endPoint.position)
            {
                Bank.Instance.AddCoins(1);
                Destroy(gameObject);
            }
        }
    }

    public void Take(Transform point)
    {
        endPoint = point;
        isTake = true;
    }

    private void TurnOnTriger() => GetComponent<SphereCollider>().enabled = true;
}
