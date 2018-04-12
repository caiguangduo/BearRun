using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_FinalScore;
        }
    }

    [SerializeField]
    private Text m_textDis;
    [SerializeField]
    private Text m_textCoin;
    [SerializeField]
    private Text m_textGoal;
    [SerializeField]
    private Text m_textScore;
    [SerializeField]
    private Slider m_sliderExp;
    [SerializeField]
    private Text m_textExp;
    [SerializeField]
    private Text m_textGrade;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void UpdateUI(int dis,int coin,int goal,int exp,int grade)
    {
        m_textDis.text = dis.ToString();
        m_textCoin.text = coin.ToString();
        m_textScore.text = (dis * (goal + 1) + coin).ToString();
        m_textGoal.text = goal.ToString();
        m_textExp.text = exp.ToString() + "/" + (500 + grade * 100).ToString();
        m_sliderExp.value = (float)exp / (500 + grade * 100);
        m_textGrade.text = grade.ToString() + "级";
    }
    public void OnReplayClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(4);
    }
    public void OnShopClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(2);
    }
    public void OnMainClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(1);
    }



    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string name, object data)
    {
    }
}
