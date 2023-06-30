using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBoxState : State
{
    private Carry carry;
    private Box box;
    private float timer;
    private float timeDelay = 0.1f;

    public CarryBoxState(Carry carry, Box box)
    {
        this.carry = carry;
        this.box = box;
    }

    public override void Update()
    {
        if (!carry.isEmptyStorage() && !box.FullBox())
        {
            timer += Time.deltaTime;
            if (timer >= timeDelay)
            {
                for (int i = 0; i < carry.items.Count; i++)
                {
                    if (carry.items[i].GetVegetableType() == box.GetBoxType())
                    {
                        Vegetable veg = carry.DeUpdateStorage(i);
                        box.GetItem(veg);
                        carry.ReCollectList();
                        break;
                    }
                    if (i == carry.items.Count - 1)
                    {
                        foreach (var item in ItemsManager.Instance.Boxes)
                        {
                            if (!item.FullBox())
                            {
                                carry.AgentCarry.SetDestination(item.transform.position);
                                carry.AnimatorCarry.SetInteger("state", 3);
                           
                            }
                        }
                    }
                }
                timer = 0f;
            }
        }
    }
}
