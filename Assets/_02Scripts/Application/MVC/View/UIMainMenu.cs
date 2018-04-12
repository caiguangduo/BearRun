using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_MainMenu;
        }
    }

    public SkinnedMeshRenderer M_PlayerSkin;
    public MeshRenderer M_BallSkin;

    private void Awake()
    {
        M_PlayerSkin.material = Game.M_Instance.M_StaticData.GetClothInfo(Game.M_Instance.M_GM.M_TakeOnSkin, Game.M_Instance.M_GM.M_TakeOnCloth).M_Material;
        M_BallSkin.material = Game.M_Instance.M_StaticData.GetFootballInfo(Game.M_Instance.M_GM.M_TakeOnFootball).M_Material;
    }

    public override void HandleEvent(string name, object data)
    {
    }

    public override void RegisterAttentionEvent()
    {
    }

    public void OnPlayClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(3);
    }
    public void OnShopClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(2);
    }
}
