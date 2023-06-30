using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxState : State
{

    private Human human;
    private Box box;
    private float timer;
    private float timeDelay = 0.1f;

    public PlayerBoxState(Human player, Box box)
    {
        this.human = player;
        this.box = box;
    }

    public override void Update()
    {

        if (!human.isEmptyStorage() && !box.FullBox())
        {
            timer += Time.deltaTime;
            if (timer >= timeDelay)
            {
                human.ThrowToBox(box);
                timer = 0f;
            }
        }
    }
}
