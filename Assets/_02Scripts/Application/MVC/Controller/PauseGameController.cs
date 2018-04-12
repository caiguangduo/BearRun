using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : Controller
{
    public override void Execute(object data)
    {
        PauseArgs e = data as PauseArgs;
        Game.M_Instance.M_GM.M_IsPause = true;
        UIPause pause = MVC.GetView<UIPause>();
        pause.Show();
        pause.m_textCoin.text = e.M_Coin.ToString();
        pause.m_textDis.text = e.M_Distance.ToString();
        pause.m_textScore.text = e.M_Score.ToString();

        PlayerAnim anim = MVC.GetView<PlayerAnim>();
        anim.StopPlayerAnim();
    }
}
