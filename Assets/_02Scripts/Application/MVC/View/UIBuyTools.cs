using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_BuyTools;
        }
    }

    public Text m_textMoney;
    public Text m_textGizmoMultiply;
    public Text m_textGizmoMagnet;
    public Text m_textGizmoInvincible;

    GameModel m_gm;

    public SkinnedMeshRenderer M_PlayerSkin;
    public MeshRenderer M_BallSkin;

    public override void HandleEvent(string name, object data)
    {

    }

    public override void RegisterAttentionEvent()
    {

    }

    private void Awake()
    {
        InitUI();
        M_PlayerSkin.material = Game.M_Instance.M_StaticData.GetClothInfo(Game.M_Instance.M_GM.M_TakeOnSkin, Game.M_Instance.M_GM.M_TakeOnCloth).M_Material;
        M_BallSkin.material = Game.M_Instance.M_StaticData.GetFootballInfo(Game.M_Instance.M_GM.M_TakeOnFootball).M_Material;
    }

    public void InitUI()
    {
        m_gm = Game.M_Instance.M_GM;

        m_textMoney.text = m_gm.M_Coin.ToString();
        ShowOrHide(m_gm.M_Magnet, m_textGizmoMagnet);
        ShowOrHide(m_gm.M_Multiply, m_textGizmoMultiply);
        ShowOrHide(m_gm.M_Invincible, m_textGizmoInvincible);


    }
    void ShowOrHide(int i,Text text)
    {
        if (i > 0)
        {
            text.transform.parent.gameObject.SetActive(true);
            text.text = i.ToString();
        }
        else
        {
            text.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnReturnClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        if (m_gm.M_LastIndex == 4)
            m_gm.M_LastIndex = 1;
        Game.M_Instance.LoadLevel(m_gm.M_LastIndex);
    }
    public void OnPlayClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(4);
    }

    public void OnMagnetClick(int i = 100)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        BuyToolsArgs e = new BuyToolsArgs
        {
            M_Coin = i,
            M_ItemKind = ItemKind.ItemMagnet
        };
        MVC.SendEvent(Consts.E_BuyToolsController, e);
    }
    public void OnInvincibleClick(int i = 200)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        BuyToolsArgs e = new BuyToolsArgs
        {
            M_Coin = i,
            M_ItemKind = ItemKind.ItemInvincible
        };
        MVC.SendEvent(Consts.E_BuyToolsController, e);
    }
    public void OnMultiplyClick(int i = 200)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        BuyToolsArgs e = new BuyToolsArgs
        {
            M_Coin = i,
            M_ItemKind = ItemKind.ItemMultiply
        };
        MVC.SendEvent(Consts.E_BuyToolsController, e);
    }
    public void OnRandomClick(int i = 300)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        int t = UnityEngine.Random.Range(0, 3);
        switch (t)
        {
            case 0:
                OnMagnetClick(i);
                break;
            case 1:
                OnInvincibleClick(i);
                break;
            case 2:
                OnMultiplyClick(i);
                break;
        }
    }
}
