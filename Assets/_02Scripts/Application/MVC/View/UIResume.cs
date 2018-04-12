using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_Resume;
        }
    }

    [SerializeField]
    private Image m_imageCount;
    public Sprite[] M_SpriteCount;

    public void StartCount()
    {
        Show();
        StartCoroutine(StartCountCoroutine());
    }
    IEnumerator StartCountCoroutine()
    {
        int i = 3;
        while (i > 0)
        {
            m_imageCount.sprite = M_SpriteCount[i - 1];
            i--;
            yield return new WaitForSeconds(1);
            if (i <= 0)
            {
                break;
            }
        }
        Hide();
        MVC.SendEvent(Consts.E_ContinueGameController);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }



    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string name, object data)
    {
    }
}
