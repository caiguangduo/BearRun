using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyClothController : Controller
{
    public override void Execute(object data)
    {
        BuySkinClothFootballArgs e = data as BuySkinClothFootballArgs;
        GameModel gm = Game.M_Instance.M_GM;
        UIShop shop = MVC.GetView<UIShop>();
        if (gm.GetMoney(e.M_Coin))
        {
            gm.M_BuyCloth.Add(e.M_ID);
            //shop.UpdateUI();
        }
    }
}
