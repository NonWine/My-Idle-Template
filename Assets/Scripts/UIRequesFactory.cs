using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIRequesFactory : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private Factory factory;

    private void Start()
    {
        factory = GetComponent<Factory>();

    }

    public void ChangeUIRequest()
    {
        text.text = factory.storageList.Count + "|" + factory.GetMaxSize();
    }

}
