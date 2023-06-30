using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCashRegisterState : State
{
    private CashRegister cashRegister;
    private Player player;
    private float timer;
    private float timeDelay =1f;
    public PlayerCashRegisterState(CashRegister cashRegister, Player player)
    {
        this.cashRegister = cashRegister;
        this.player = player;
    }

    public override void Enter()
    {
      
    }

    public override void Update()
    {
        if (!cashRegister.isEmpty())
        {
            timer += Time.deltaTime;
            if(timer >= timeDelay)
            {
                CardBoardBox cardBoardBox = cashRegister.CreateBox();
                cashRegister.GetCustomer().GetCashState().BuyItems(cardBoardBox);
                timer = 0f;
            }
          

        }
    }

    public override void Exit()
    {
        player.SetIdleState();
    }
}
