using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = data as ItemArgs;
        PlayerMove player = MVC.GetView<PlayerMove>();
        UIBoard uiBoard = MVC.GetView<UIBoard>();
        switch (e.M_Kind)
        {
            case ItemKind.ItemMagnet:
                player.HitMagnet();
                Game.M_Instance.M_GM.M_Magnet -= e.M_HitCount;
                uiBoard.HitMagnet();
                break;
            case ItemKind.ItemMultiply:
                player.HitMultiply();
                Game.M_Instance.M_GM.M_Multiply -= e.M_HitCount;
                uiBoard.HitMultiply();
                break;
            case ItemKind.ItemInvincible:
                player.HitInvincible();
                Game.M_Instance.M_GM.M_Invincible -= e.M_HitCount;
                uiBoard.HitInvincible();
                break;
        }
        uiBoard.UpdateUI();
    }
}
