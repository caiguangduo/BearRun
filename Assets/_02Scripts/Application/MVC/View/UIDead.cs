using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_Dead;
        }
    }

    private int m_briberyTime = 1;
    public int M_BriberyTime
    {
        get
        {
            return m_briberyTime;
        }
        set
        {
            m_briberyTime = value;
        }
    }

    public Text m_textBribery;

    private void Awake()
    {
        M_BriberyTime = 1;
    }

    public void Show()
    {
        m_textBribery.text = (500 * M_BriberyTime).ToString();
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnCancleClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        MVC.SendEvent(Consts.E_FinalShowUIController);
    }
    public void OnBriberyClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        CoinArgs e = new CoinArgs
        {
            M_Coin = M_BriberyTime * 500
        };
        MVC.SendEvent(Consts.E_BriberyClickController, e);
    }



    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string name, object data)
    {
    }
}
