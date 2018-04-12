using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : Controller
{
    public override void Execute(object data)
    {
        Game.M_Instance.M_GM.M_IsPlay = false;

        UIDead dead = MVC.GetView<UIDead>();
        dead.Show();

        PlayerAnim anim = MVC.GetView<PlayerAnim>();
        anim.StopPlayerAnim();
    }
}
