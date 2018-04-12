using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFootballController : Controller
{
    public override void Execute(object data)
    {
        UIShop shop = MVC.GetView<UIShop>();
        int id = (int)data;
        GameModel gm = Game.M_Instance.M_GM;
        gm.M_TakeOnFootball = id;

        //shop.UpdateFootballBuyButton(id);
        //shop.UpdateFootballGizmo();
    }
}
