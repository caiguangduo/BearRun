using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriberyClickController : Controller
{
    public override void Execute(object data)
    {
        CoinArgs e = data as CoinArgs;
        UIDead dead = MVC.GetView<UIDead>();

        if (Game.M_Instance.M_GM.GetMoney(e.M_Coin))
        {
            dead.Hide();
            dead.M_BriberyTime++;
            UIResume resume = MVC.GetView<UIResume>();
            resume.StartCount();
        }
    }
}
