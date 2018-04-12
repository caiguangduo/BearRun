using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameController : Controller
{
    public override void Execute(object data)
    {
        UIBoard board = MVC.GetView<UIBoard>();
        if (board.M_Timer < 0.1f)
        {
            board.M_Timer += 20;
        }
        Game.M_Instance.M_GM.M_IsPause = false;
        Game.M_Instance.M_GM.M_IsPlay = true;

        PlayerAnim anim = MVC.GetView<PlayerAnim>();
        anim.ContinuePlay();
    }
}
