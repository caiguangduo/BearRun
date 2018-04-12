using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
    public const int m_startTime = 50;

    public override string M_Name
    {
        get
        {
            return Consts.V_Board;
        }
    }
    
    [SerializeField]
    private Text m_textDistance;
    private int m_distance = 0;
    public int M_Distance
    {
        get
        {
            return m_distance;
        }
        set
        {
            m_distance = value;
            m_textDistance.text = value.ToString() + "米";
        }
    }

    [SerializeField]
    private Text m_textCoin;
    private int m_coin = 0;
    public int M_Coin
    {
        get
        {
            return m_coin;
        }
        set
        {
            m_coin = value;
            m_textCoin.text = m_coin.ToString();
        }
    }

    [SerializeField]
    private Text m_textTimer;
    [SerializeField]
    private Slider m_sliderTimer;
    private float m_time;
    public float M_Timer
    {
        get
        {
            return m_time;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
                MVC.SendEvent(Consts.E_EndGameController);
            }
            if (value > m_startTime)
            {
                value = m_startTime;
            }
            m_time = value;
            m_textTimer.text = m_time.ToString("f2") + "s";
            m_sliderTimer.value = value / m_startTime;
        }
    }

    private int m_goalCount = 0;
    public int M_GoalCount
    {
        get { return m_goalCount; }
        set { m_goalCount = value; }
    }

    [SerializeField]
    private Button m_magnetBtn;
    [SerializeField]
    private Button m_multiplyBtn;
    [SerializeField]
    private Button m_invincibleBtn;
    private IEnumerator m_multiplyCoroutine;
    private IEnumerator m_magnetCoroutine;
    private IEnumerator m_invincibleCoroutine;
    [SerializeField]
    private Text m_textGizmoMultiply;
    [SerializeField]
    private Text m_textGizmoMagnet;
    [SerializeField]
    private Text m_textGizmoInvincible;
    private int m_skillTime
    {
        get { return Game.M_Instance.M_GM.M_SkillTime; }
    }

    [SerializeField]
    private Button m_btnGoal;
    [SerializeField]
    private Slider m_sliderGoal;

    private void Awake()
    {
        M_Timer = m_startTime;

        UpdateUI();
    }
    private void Update()
    {
        if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
        {
            M_Timer -= Time.deltaTime;
        }
    }

    public override void RegisterAttentionEvent()
    {
        M_AttentionList.Add(Consts.E_UpdateDisAttention);
        M_AttentionList.Add(Consts.E_UpdateCoinAttention);
        M_AttentionList.Add(Consts.E_HitAddTimeAttention);
        M_AttentionList.Add(Consts.E_HitGoalTriggerAttention);
        M_AttentionList.Add(Consts.E_ShootGoalAttention);
    }
    public override void HandleEvent(string name, object data)
    {
        switch (name)
        {
            case Consts.E_UpdateDisAttention:
                DistanceArgs e1 = data as DistanceArgs;
                M_Distance = e1.M_Distance;
                break;
            case Consts.E_UpdateCoinAttention:
                CoinArgs e2 = data as CoinArgs;
                M_Coin += e2.M_Coin;
                break;
            case Consts.E_HitAddTimeAttention:
                M_Timer += 20;
                break;
            case Consts.E_HitGoalTriggerAttention:
                ShowGoalClick();
                break;
            case Consts.E_ShootGoalAttention:
                M_GoalCount += 1;
                break;
        }
    }
    void ShowGoalClick()
    {
        StartCoroutine(StartCountDown());
    }
    IEnumerator StartCountDown()
    {
        m_btnGoal.interactable = true;
        m_sliderGoal.value = 1;
        while (m_sliderGoal.value > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                m_sliderGoal.value -= 0.85f * Time.deltaTime;
            }
            yield return null;
        }
        m_btnGoal.interactable = false;
        m_sliderGoal.value = 0;
    }


    public void OnPauseClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        PauseArgs e = new PauseArgs
        {
            M_Coin = this.M_Coin,
            M_Distance = this.M_Distance,
            M_Score = this.M_Coin + this.M_Distance * (M_GoalCount+1)
        };
        MVC.SendEvent(Consts.E_PauseGameController,e);
    }

    public void UpdateUI()
    {
        ShowOrHide(Game.M_Instance.M_GM.M_Invincible, m_invincibleBtn);
        ShowOrHide(Game.M_Instance.M_GM.M_Magnet, m_magnetBtn);
        ShowOrHide(Game.M_Instance.M_GM.M_Multiply, m_multiplyBtn);
    }
    void ShowOrHide(int i,Button btn)
    {
        if (i > 0)
            btn.interactable = true;
        else
            btn.interactable = false;
    }

    public void OnMagnetClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        ItemArgs e = new ItemArgs
        {
            M_HitCount = 1,
            M_Kind = ItemKind.ItemMagnet
        };
        MVC.SendEvent(Consts.E_HitItemController, e);
    }
    public void OnMultiplyClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        ItemArgs e = new ItemArgs
        {
            M_HitCount = 1,
            M_Kind = ItemKind.ItemMultiply
        };
        MVC.SendEvent(Consts.E_HitItemController, e);
    }
    public void OnInvincibleClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        ItemArgs e = new ItemArgs
        {
            M_HitCount = 1,
            M_Kind = ItemKind.ItemInvincible
        };
        MVC.SendEvent(Consts.E_HitItemController, e);
    }

    string GetTime(float time)
    {
        return ((int)time + 1).ToString();
    }
    public void HitMultiply()
    {
        if (m_multiplyCoroutine != null)
            StopCoroutine(m_multiplyCoroutine);
        m_multiplyCoroutine = MultiplyCoroutine();
        StartCoroutine(m_multiplyCoroutine);
    }
    IEnumerator MultiplyCoroutine()
    {
        float timer = m_skillTime;
        m_textGizmoMultiply.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
                m_textGizmoMultiply.text = GetTime(timer);
            }
            yield return null;
        }
        m_textGizmoMultiply.transform.parent.gameObject.SetActive(false);
    }

    public void HitMagnet()
    {
        if (m_magnetCoroutine != null)
            StopCoroutine(m_magnetCoroutine);
        m_magnetCoroutine = MagnetCoroutine();
        StartCoroutine(m_magnetCoroutine);
    }
    IEnumerator MagnetCoroutine()
    {
        m_textGizmoMagnet.transform.parent.gameObject.SetActive(true);
        float timer = m_skillTime;
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
                m_textGizmoMagnet.text = GetTime(timer);
            }
            yield return null;
        }
        m_textGizmoMagnet.transform.parent.gameObject.SetActive(false);
    }

    public void HitInvincible()
    {
        if (m_invincibleCoroutine != null)
            StopCoroutine(m_invincibleCoroutine);
        m_invincibleCoroutine = InvincibleCoroutine();
        StartCoroutine(m_invincibleCoroutine);
    }
    IEnumerator InvincibleCoroutine()
    {
        float timer = m_skillTime;
        m_textGizmoInvincible.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (Game.M_Instance.M_GM.M_IsPlay && !Game.M_Instance.M_GM.M_IsPause)
            {
                timer -= Time.deltaTime;
                m_textGizmoInvincible.text = GetTime(timer);
            }
            yield return null;
        }
        m_textGizmoInvincible.transform.parent.gameObject.SetActive(false);
    }

    public void OnGoalBtnClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        MVC.SendEvent(Consts.E_ClickGoalButtonAttention);
        m_btnGoal.interactable = false;
        m_sliderGoal.value = 0;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
