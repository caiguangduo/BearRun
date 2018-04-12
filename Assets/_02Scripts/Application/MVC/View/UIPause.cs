using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_Pause;
        }
    }

    public Text m_textDis;
    public Text m_textCoin;
    public Text m_textScore;

    [SerializeField]
    private SkinnedMeshRenderer skm;
    [SerializeField]
    private MeshRenderer render;

    public void Show()
    {
        gameObject.SetActive(true);

        UpdateUI();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    void UpdateUI()
    {
        //TODO:

    }

    public void OnResumeClick()
    {
        Hide();
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        MVC.SendEvent(Consts.E_ResumeGameController);
    }
    public void OnHomeClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        Game.M_Instance.LoadLevel(1);
    }
    private void Awake()
    {
        UpdateUI();
    }



    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string name, object data)
    {
    }
}
