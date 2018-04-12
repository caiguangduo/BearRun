using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSkinController : Controller
{
    public override void Execute(object data)
    {
        UIShop shop = MVC.GetView<UIShop>();
        int id = (int)data;
        GameModel gm = Game.M_Instance.M_GM;
        gm.M_TakeOnSkin = id;
        shop.UpdateUI();
    }
}
