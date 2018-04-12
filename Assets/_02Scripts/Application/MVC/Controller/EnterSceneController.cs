using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.M_SceneIndex)
        {
            case 1:
                Game.M_Instance.M_Sound.PlayBG(Consts.S_BgJieMian);
                MVC.RegisterView(GameObject.Find("Canvas").GetComponentInChildren<UIMainMenu>());
                break;
            case 2:
                Game.M_Instance.M_Sound.PlayBG(Consts.S_BgJieMian);
                MVC.RegisterView(GameObject.Find("Canvas").GetComponent<UIShop>());
                break;
            case 3:
                Game.M_Instance.M_Sound.PlayBG(Consts.S_BgJieMian);
                MVC.RegisterView(GameObject.Find("Canvas").transform.Find("UIBuyTools").GetComponent<UIBuyTools>());
                break;
            case 4:
                Game.M_Instance.M_Sound.PlayBG(Consts.S_BgZhanDou);

                GameObject player = GameObject.FindWithTag(Tag.Tag_Player);
                MVC.RegisterView(player.GetComponent<PlayerMove>());
                MVC.RegisterView(player.GetComponent<PlayerAnim>());

                Transform canvas = GameObject.Find("Canvas").transform;
                MVC.RegisterView(canvas.Find("UIBoard").GetComponent<UIBoard>());
                MVC.RegisterView(canvas.Find("UIPause").GetComponent<UIPause>());
                MVC.RegisterView(canvas.Find("UIResume").GetComponent<UIResume>());
                MVC.RegisterView(canvas.Find("UIDead").GetComponent<UIDead>());
                MVC.RegisterView(canvas.Find("UIFinalScore").GetComponent<UIFinalScore>());

                Game.M_Instance.M_GM.M_IsPause = false;
                Game.M_Instance.M_GM.M_IsPlay = true;
                break;
        }
    }
}
