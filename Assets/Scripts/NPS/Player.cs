using UnityEngine;
using UniRx;
public class Player : Human
{
    public static Player Instance { get; private set; }
    [SerializeField] private CameraFollowing cameraFollow;
    private CompositeDisposable disposable = new CompositeDisposable();
    public Animator PlayerAnimator { get; private set; }
    private void Awake()
    {
        Instance = this;
        
    }

    protected override void Start()
    {
        base.Start();
        PlayerAnimator = animator;
        Bank.Instance.AddCoins(1000); 
    }

    protected override void Update()
    {
        base.Update();
     //   Debug.Log(stateMachine.currentState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plant"))
        {
            stateMachine.ChangeState(new PlayerPlantState(this,other.GetComponent<Plant>()));
            
        }
        else if (other.CompareTag("Box") && currentStorage >= 0)
        {
            Box box = other.GetComponent<Box>();
            stateMachine.ChangeState(new PlayerBoxState(this, box));
            Debug.Log("player");
        }
        else if (other.CompareTag("CashRegister"))
        {
            CashRegister cash = other.GetComponent<CashRegister>();
            stateMachine.ChangeState(new PlayerCashRegisterState(cash, this));
        }
        if (other.CompareTag("Dollar"))
        {
            other.GetComponent<Dollar>().Take(itemPoints[0]);
        }
        if (other.CompareTag("BuyZona"))
        {

            Observable.EveryUpdate().Subscribe(_ => { other.GetComponent<BuyZona>().BuyPlant(); }).AddTo(disposable);
        }
        if (other.CompareTag("Machine"))
        {
            stateMachine.ChangeState(new MachineState(this,other.GetComponent<Machine>()));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plant"))
            SetIdleState();
        if (other.CompareTag("Box"))
            stateMachine.ChangeState(new IdleState());

        if (other.CompareTag("CashRegister"))
        {
            stateMachine.currentState.Exit();
        }
        if (other.CompareTag("BuyZona"))
            disposable.Clear();
    }


    public void ClearBuyDispose() => disposable.Clear();

    public override void UpdateStorage(Vegetable item)
    {
        animator.SetBool("HaveItem", true);
        items.Add(item);
        currentStorage++;
        ReCollectList();
    }


    public override Vegetable DeUpdateStorage(int i)
    {

        if (!isEmptyStorage())
        {
            //animator.SetBool("HaveItem", false);
            Vegetable veg = items[i];
            items.RemoveAt(i);
            currentStorage--;
            if (isEmptyStorage())
                animator.SetBool("HaveItem", false);
            ReCollectList();
            return veg;
        }
        else
            return null;
    }

    public override void ThrowToBox(Box box)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetVegetableType() == box.GetBoxType())
            {
                Vegetable veg = DeUpdateStorage(i);
                box.GetItem(veg);
                break;
            }

        }
    }

}