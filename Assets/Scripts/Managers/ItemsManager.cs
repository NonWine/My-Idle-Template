using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance { get; private set; }

    [SerializeField] private List<Box> boxes;
    public List<Plant> Plants { get;  set; }

    public Transform HomePoint;

    public List<Box> Boxes => boxes;
    
    private void Awake()
    {
        Instance = this;
        Plants = new List<Plant>();
        boxes = new List<Box>();
    }

    public Transform GetBox(VegetableType type)
    {
        switch (type)
        {
            case VegetableType.Tomato:
                return boxes[0].GetCustomersPoints();
            case VegetableType.Eeg:
                return boxes[1].GetCustomersPoints();
            case VegetableType.Tykva:
                return boxes[2].GetCustomersPoints();
            default:
                return null;
        }
    }
}
