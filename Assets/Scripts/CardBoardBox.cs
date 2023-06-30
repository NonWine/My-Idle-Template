using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardBox : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private Vector3 startPos;
    private int index;
    private void Start()
    {
        startPos = transform.position;
    }

    public Transform GetPoint(int i)
    {

        return points[i];
    }

    public Vector3 GetStartPos() { return startPos; }

}
