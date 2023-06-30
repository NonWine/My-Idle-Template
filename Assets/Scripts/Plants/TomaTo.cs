using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaTo : Plant
{

    protected override void Start()
    {
        base.Start();
        SpawnRx();
    }

    public override void Spawn()
    {

        timer += Time.deltaTime;
        if (timer >= timerSpawn)
        {
            Vegetable veg = Instantiate(vegetable, spawnItemsPoints[itemList.Count].position, Quaternion.identity);
            itemList.Push(veg);
            timer = 0f;
        }
        if (isItemsMax())
        {
            disposable.Clear();
            timer = 0f;
        }


    }

    public override void GiveItems(Human human)
    {
        if (isHaveItem() && !human.isMaxStorage())
        {
            giveItemsTimer += Time.deltaTime;
            if (giveItemsTimer >= 0.1f && CheckVeg().isActive)
            {
                giveItemsTimer = 0f;
                Vegetable veg = GetVegetable();
                veg.transform.SetParent(human.transform);
                veg.ThrowIT(human.GetPoint(), 6);
                human.UpdateStorage(veg);
                human.ReCollectList();
                SpawnRx();
            }

        }
    }


}
