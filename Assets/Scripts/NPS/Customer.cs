using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class Customer : Human
{
    [SerializeField] private Transform boxPlace;
    [SerializeField] private VegetableType type;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private TMP_Text text;
    private NavMeshAgent agent;
    private Transform boxPoint;
    private Transform homePoint;
    private CashRegister cashRegister;
    private bool trig;

    public Animator AnimCustomer { get; private set; }

    public NavMeshAgent AgentCustomer { get; private set; }

    public CashRegister CusstomerCashRegister { get; set; }

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        maxStorage = Random.Range(1, 3);
        type = (VegetableType)Random.Range(0, (int)CustomerSpawner.Instance.vegetableType + 1);
        SetPoints(ItemsManager.Instance.GetBox(type), ItemsManager.Instance.HomePoint);
        animator.SetInteger("state", 1);
        agent.SetDestination(boxPoint.position);
        CusstomerCashRegister = cashRegister;
        AnimCustomer = animator;
        AgentCustomer = agent;
        text.text = currentStorage.ToString() + "|" + GetMaxStorage().ToString();
    }

    protected override void Update()
    {
        base.Update();
      
       _canvas.rotation = Quaternion.Euler(-15f, transform.rotation.y - 180f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trig)
        {
            if (other.CompareTag("Box") && currentStorage < maxStorage)
            {
                if (other.GetComponent<Box>().GetBoxType() == type)
                    stateMachine.ChangeState(new BoxState(this, other.GetComponent<Box>()));
                else
                    return;
            }
            else if (other.CompareTag("CashRegister") && currentStorage == maxStorage)
            {
                stateMachine.ChangeState(new CashRegiterState(this, other.GetComponent<CashRegister>()));
              
            }

            if (other.CompareTag("Home"))
            {
                Destroy(gameObject);
            }
        }
   
            
    }

    public override void UpdateStorage(Vegetable item)
    {
        base.UpdateStorage(item);
        text.text = currentStorage.ToString() + "|" + GetMaxStorage().ToString();
    }

    public override Vegetable DeUpdateStorage(int i)
    {

        if (!isEmptyStorage())
        {
            Vegetable veg = items[i];
            items.RemoveAt(i);
            currentStorage--;
            text.text = currentStorage.ToString() + "|" + GetMaxStorage().ToString();
            return veg;
        }
        else
            return null;
    }


        public StateMachine GetCustomerStatmeMachine() { return stateMachine; }


    public Transform GetBoPlace() { return boxPlace; }

    public Transform GetHomePoint() { return homePoint; }

    public BoxState GetBoxState() { return stateMachine.currentState as BoxState; }

    public CashRegiterState GetCashState() { return stateMachine.currentState as CashRegiterState; }

    public void SetPoints(Transform box, Transform home)
    {
        boxPoint = box;
        homePoint = home;
    }

    public void SetTrig(bool flag) => trig = flag;

    public void SetCashRegister(CashRegister cash) => cashRegister = cash;

    public VegetableType GetItemType() { return type; }

    public int GetMaxStorage() { return maxStorage; }
}
