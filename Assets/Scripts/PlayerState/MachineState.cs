using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineState : State
{
    private Player player;
    private Machine machine;

    public MachineState(Player player, Machine machine)
    {
        this.machine = machine;
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        machine.GiveItems(player);
    }
}
