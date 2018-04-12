using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : ReusableObject
{
    public override void OnSpawn()
    {
    }

    public override void OnUnSpawn()
    {
        var itemChild = transform.Find("Item");
        if (itemChild != null)
        {
            if (itemChild.childCount > 0)
            {
                for (int i = 0; i < itemChild.childCount; i++)
                {
                    GameObject go = itemChild.GetChild(i).gameObject;
                    if(go.activeSelf)
                        Game.M_Instance.M_ObjectPool.UnSpawn(go);
                }
            }
        }
    }
}
