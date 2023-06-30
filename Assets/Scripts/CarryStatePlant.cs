using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryStatePlant : State
{
    private  Carry carry;
    private Plant plant;

    public CarryStatePlant(Carry carry, Plant plant)
    {
        this.carry = carry;
        this.plant = plant;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (plant.isHaveItem())
        {
            plant.GiveItems(carry);
        }   
    }
}
