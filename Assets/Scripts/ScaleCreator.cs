using System.Collections;
using UnityEngine;

public class ScaleCreator : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float _size = 1f;
    [SerializeField] private float _time;
    private Vector3 _startPoint;
    private Vector3 _endSize;
    private float timer;

    private void Start()
    {
       
    }

    public void StartScaleCreator() 
    {
        _endSize = new Vector3(_size, _size, _size);
        _startPoint = Vector3.zero;
        StartCoroutine(CreateByScale());
    }

    IEnumerator CreateByScale()
    {
        do
        {
            obj.localScale = Vector3.Lerp(_startPoint, _endSize, ((timer + (_time / 10f)) / _time));
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        while (timer <= _time);
    }
}
