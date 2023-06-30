using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner Instance { get; private set; }
    [SerializeField] private Customer prefab;
    [SerializeField] private Transform spawnPoint;
    [Header("Points For Nav Customers")]
    [SerializeField] private Transform cashRegisterPoint;
    [SerializeField] private Transform homePoint;
    [SerializeField] private int _maxCount;
    [SerializeField] private CashRegister cash;
    private float timer;
    private float timeDelay = 10f;
    private int startMaxCount;
    public VegetableType vegetableType { get;  set; }

    private void Awake()
    {
        Instance = this;
        timer = timeDelay;
        vegetableType = VegetableType.Tomato;
        vegetableType = (VegetableType)PlayerPrefs.GetInt("vegetableType", (int)vegetableType);
        _maxCount = PlayerPrefs.GetInt("_maxCount", _maxCount);
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeDelay && startMaxCount < _maxCount)
        {
            timer = 0f;
            startMaxCount++;
            Customer customer = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            customer.SetCashRegister(cash);
        }
    }

    public void ClearFromWait()
    {
        startMaxCount--;
    }

    public void AddMaxCount()
    {
        _maxCount++;
        PlayerPrefs.SetInt("_maxCount", _maxCount);

    }

    public void SetNewVegType(VegetableType type)
    {
        vegetableType = type;
        PlayerPrefs.SetInt("vegetableType", (int)vegetableType);
    }
  
}
