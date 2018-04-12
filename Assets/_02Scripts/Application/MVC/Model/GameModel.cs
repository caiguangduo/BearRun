using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{
    const int m_initCoin = 5000;

    public override string M_Name
    {
        get
        {
            return Consts.M_GameModel;
        }
    }

    private bool m_isPlay = true;
    private bool m_isPause=false;
    public bool M_IsPlay
    {
        get
        {
            return m_isPlay;
        }

        set
        {
            m_isPlay = value;
        }
    }
    public bool M_IsPause
    {
        get
        {
            return m_isPause;
        }

        set
        {
            m_isPause = value;
        }
    }

    private int m_lastIndex = 1;
    public int M_LastIndex
    {
        get
        {
            return m_lastIndex;
        }
        set
        {
            if (value < 0 || value > 5)
                value = 0;
            m_lastIndex = value;
        }
    }

    private int m_multiply;
    public int M_Multiply
    {
        get
        {
            return m_multiply;
        }
        set
        {
            m_multiply = value;
        }
    }

    private int m_skillTime = 5;
    public int M_SkillTime
    {
        get
        {
            return m_skillTime;
        }
        set
        {
            m_skillTime = value;
        }
    }

    private int m_magnet;
    public int M_Magnet
    {
        get
        {
            return m_magnet;
        }
        set
        {
            m_magnet = value;
        }
    }

    private int m_invincible;
    public int M_Invincible
    {
        get
        {
            return m_invincible;
        }
        set
        {
            m_invincible = value;
        }
    }

    private int m_grade;
    public int M_Grage
    {
        get
        {
            return m_grade;
        }
        set
        {
            m_grade = value;
        }
    }

    private int m_exp;
    public int M_Exp
    {
        get
        {
            return m_exp;
        }
        set
        {
            while (value > (500 + M_Grage * 100))
            {
                value -= (500 + M_Grage * 100);
                M_Grage++;
            }
            m_exp = value;
        }
    }

    private int m_coin;
    public int M_Coin
    {
        get
        {
            return m_coin;
        }
        set
        {
            m_coin = value;
        }
    }

    private int m_takeOnFootball = 0;
    public int M_TakeOnFootball
    {
        get { return m_takeOnFootball; }
        set { m_takeOnFootball = value; }
    }
    public List<int> M_BuyFootBall = new List<int>();

    private int m_takeOnCloth = 0;
    public int M_TakeOnCloth
    {
        get { return m_takeOnCloth; }
        set { m_takeOnCloth = value; }
    }
    public List<int> M_BuyCloth = new List<int>();

    private int m_takeOnSkin = 0;
    public int M_TakeOnSkin
    {
        get { return m_takeOnSkin; }
        set { m_takeOnSkin = value; }
    }
    public List<int> M_BuySkin = new List<int>();

    public void Init()
    {
        M_Magnet = 0;
        M_Invincible = 0;
        M_Multiply = 0;
        M_SkillTime = 5;

        M_Exp = 0;
        M_Grage = 1;
        M_Coin = m_initCoin;

        InitSkin();
    }

    void InitSkin()
    {
        M_BuyFootBall.Add(0);
        M_BuyCloth.Add(0);
        M_BuySkin.Add(0);
    }

    public bool GetMoney(int coin)
    {
        if (coin <= M_Coin)
        {
            M_Coin -= coin;
            return true;
        }
        return false;
    }

    public ItemState CheckFootballState(int i)
    {
        if (i == M_TakeOnFootball)
        {
            return ItemState.Equip;
        }
        else
        {
            if (M_BuyFootBall.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }
    public ItemState CheckClothState(int i)
    {
        if (i == M_TakeOnCloth)
        {
            return ItemState.Equip;
        }
        else
        {
            if (M_BuyCloth.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }
    public ItemState CheckSkinState(int i)
    {
        if (i == M_TakeOnSkin)
        {
            return ItemState.Equip;
        }
        else
        {
            if (M_BuySkin.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }

}
