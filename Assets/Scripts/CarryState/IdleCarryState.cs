using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCarryState : State
{
    private Carry carry;

    public IdleCarryState(Carry carry) => this.carry = carry;

    public override void Enter()
    {
        carry.StartEveryUpdate(carry.Work);
    }

    public override void Update()
    {
        carry.Work();
    }

}
