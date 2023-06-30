using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory 
{
    public void AddinStorageList(Vegetable obj);
    public void PlayerGiveItems(Human player);

    public void DeleteController();
}
