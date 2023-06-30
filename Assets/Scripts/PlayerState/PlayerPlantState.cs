using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantState : State
{
    Human human;
    Plant plant;

    public PlayerPlantState(Human human, Plant vegetable)
    {
        this.human = human;
        this.plant = vegetable;
    }


    public override void Update()
    {
      
        plant.GiveItems(human);
    }
}
