using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    [SerializeField] private VegetableType vegetableType;
    [SerializeField] private float speedLerp = 6f;
    private Transform endPoint;
    private Vector3 startPoint;
    private float timer;
    private bool canMove;
    public bool isActive { get; private set; }
    private void Start()
    {
        startPoint = transform.position;
        Invoke(nameof(TurnActive), 0.5f);
    }


    private void Update()
    {
        if (canMove)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, endPoint.position, speedLerp * timer);
            if (transform.position == endPoint.position)
            {
                canMove = false;
            }

        }
       
    }

    public void TurnActive() => isActive = true;

    public void TakeIt(Player player)
    {
        transform.SetParent(player.transform);
        endPoint = player.GetPoint();    
        canMove = true;
        player.UpdateStorage(this);
    }
    
    public void ThrowIT(Transform point, float s = 3f)
    {
        speedLerp = s;
        timer = 0f;
        startPoint = transform.position;
        endPoint = point;
        canMove = true;
    }
    
    public bool GetCanMove()
    {
        return canMove;
    }

    public VegetableType GetVegetableType() { return vegetableType; }

}
public enum VegetableType {Tomato, Eeg, Tykva }