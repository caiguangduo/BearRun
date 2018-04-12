using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    public override string M_Name
    {
        get
        {
            return Consts.V_Shop;
        }
    }

    public override void HandleEvent(string name, object data)
    {
    }
    public override void RegisterAttentionEvent()
    {
    }

    GameModel m_gm;

    public MeshRenderer M_Ball;

    private int m_selectIndex;
    private ItemState state;

    public SkinnedMeshRenderer M_PlayerSkm;
    public Image M_ImageBuySkin;
    public Sprite M_SpBuy;
    public Sprite M_SpEquip;
    public List<Image> M_SkinGizmo;
    public Sprite M_GizmoBuy;
    public Sprite M_GizmoUnBuy;
    public Sprite M_GizmoEquip;
    public Text M_TextMoney;
    public Text M_TextName;
    public Image M_HeadShow;
    public List<Sprite> M_Head;
    public Image M_ImageBuyCloth;
    public List<Image> M_ClothGizmo;
    public Image M_ImageBuyFootball;
    public List<Image> M_FootballGizmo;

    public Text m_textGrade;

    public List<Toggle> M_SkinToggles;

    private void Awake()
    {
        m_gm = Game.M_Instance.M_GM;
        //M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetSkinInfo(m_gm.M_TakeOnSkin).M_Material;
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_gm.M_TakeOnCloth).M_Material;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_gm.M_TakeOnFootball).M_Material;
        UpdateUI();
        UpdateSkinGizmo();
        m_textGrade.text = m_gm.M_Grage.ToString();

        for (int i = 0; i < M_SkinToggles.Count; i++)
        {
            if (i == m_gm.M_TakeOnSkin)
                M_SkinToggles[i].isOn = true;
            else
                M_SkinToggles[i].isOn = false;
        }
    }

    public void OnGroupSkin()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_gm.M_TakeOnCloth).M_Material;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_gm.M_TakeOnFootball).M_Material;
        UpdateSkinGizmo();
        Hide();
    }
    public void OnGroupCloth()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_gm.M_TakeOnCloth).M_Material;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_gm.M_TakeOnFootball).M_Material;
        UpdateClothGizmo();
        Hide();
    }
    public void OnGroupFootBall()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        UpdateFootballGizmo();
        Hide();
    }
    void Hide()
    {
        M_ImageBuySkin.gameObject.SetActive(false);
        M_ImageBuyCloth.gameObject.SetActive(false);
        M_ImageBuyFootball.gameObject.SetActive(false);
    }
    public void OnPlayGame()
    {
        Game.M_Instance.LoadLevel(3);
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
    }
    public void OnReturnClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        if (m_gm.M_LastIndex == 4)
            m_gm.M_LastIndex = 1;
        Game.M_Instance.LoadLevel(m_gm.M_LastIndex);
    }

    public void OnMoMoClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 0;
        //M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetSkinInfo(m_selectIndex).M_Material;
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_selectIndex, m_gm.M_TakeOnCloth).M_Material;
        //state = m_gm.CheckSkinState(m_selectIndex);
        UpdateBuySkinButton(0);
    }
    public void OnSaliClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 1;
        //M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetSkinInfo(m_selectIndex).M_Material;
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_selectIndex, m_gm.M_TakeOnCloth).M_Material;
        //state = m_gm.CheckSkinState(m_selectIndex);
        UpdateBuySkinButton(1);
    }
    public void OnSugarClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 2;
        //M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetSkinInfo(m_selectIndex).M_Material;
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_selectIndex, m_gm.M_TakeOnCloth).M_Material;
        //state = m_gm.CheckSkinState(m_selectIndex);
        UpdateBuySkinButton(2);
    }
    public void UpdateBuySkinButton(int i)
    {
        state = m_gm.CheckSkinState(i);
        switch (state)
        {
            case ItemState.UnBuy:
                M_ImageBuySkin.gameObject.SetActive(true);
                M_ImageBuySkin.overrideSprite = M_SpBuy;
                break;
            case ItemState.Buy:
                M_ImageBuySkin.gameObject.SetActive(true);
                M_ImageBuySkin.overrideSprite = M_SpEquip;
                break;
            case ItemState.Equip:
                M_ImageBuySkin.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void OnBuySkinClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        state = m_gm.CheckSkinState(m_selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                int id = m_selectIndex;
                int money = Game.M_Instance.M_StaticData.GetSkinInfo(m_selectIndex).M_Coin;
                BuySkinClothFootballArgs e = new BuySkinClothFootballArgs()
                {
                    M_Coin = money,
                    M_ID = id
                };
                MVC.SendEvent(Consts.E_BuySkinController, e);
                break;
            case ItemState.Buy:
                MVC.SendEvent(Consts.E_EquipSkinController, m_selectIndex);
                break;
        }
        UpdateBuySkinButton(m_selectIndex);
        UpdateSkinGizmo();
    }
    public void UpdateSkinGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = m_gm.CheckSkinState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    M_SkinGizmo[i].overrideSprite = M_GizmoUnBuy;
                    break;
                case ItemState.Buy:
                    M_SkinGizmo[i].overrideSprite = M_GizmoBuy;
                    break;
                case ItemState.Equip:
                    M_SkinGizmo[i].overrideSprite = M_GizmoEquip;
                    break;
            }
        }
    }

    public void UpdateUI()
    {
        M_TextMoney.text = m_gm.M_Coin.ToString();
        switch (m_gm.M_TakeOnSkin)
        {
            case 0:
                M_TextName.text = "MoMo";
                break;
            case 1:
                M_TextName.text = "SaLi";
                break;
            case 2:
                M_TextName.text = "Suger";
                break;
        }
        M_HeadShow.overrideSprite = M_Head[m_gm.M_TakeOnSkin];
    }

    public void OnNormalClick()
    {
        m_selectIndex = 0;
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_selectIndex).M_Material;
        UpdateBuyClothButton(0);
    }
    public void OnBrizalClick()
    {
        m_selectIndex = 1;
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_selectIndex).M_Material;
        UpdateBuyClothButton(1);
    }
    public void OnGermanClick()
    {
        m_selectIndex = 2;
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        M_PlayerSkm.material = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_selectIndex).M_Material;
        UpdateBuyClothButton(2);
    }
    public void UpdateBuyClothButton(int i)
    {
        state = m_gm.CheckClothState(i);
        switch (state)
        {
            case ItemState.UnBuy:
                M_ImageBuyCloth.gameObject.SetActive(true);
                M_ImageBuyCloth.overrideSprite = M_SpBuy;
                break;
            case ItemState.Buy:
                M_ImageBuyCloth.gameObject.SetActive(true);
                M_ImageBuyCloth.overrideSprite = M_SpEquip;
                break;
            case ItemState.Equip:
                M_ImageBuyCloth.gameObject.SetActive(false);
                break;
        }
    }
    public void OnBuyClothClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        state = m_gm.CheckClothState(m_selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                int money01 = Game.M_Instance.M_StaticData.GetClothInfo(m_gm.M_TakeOnSkin, m_selectIndex).M_Coin;
                BuySkinClothFootballArgs e = new BuySkinClothFootballArgs()
                {
                    M_Coin = money01,
                    M_ID = m_selectIndex
                };
                MVC.SendEvent(Consts.E_BuyClothController, e);
                break;
            case ItemState.Buy:
                MVC.SendEvent(Consts.E_EquipClothController, m_selectIndex);
                break;
        }
        UpdateBuyClothButton(m_selectIndex);
        UpdateClothGizmo();
    }
    public void UpdateClothGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = m_gm.CheckClothState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    M_ClothGizmo[i].overrideSprite = M_GizmoUnBuy;
                    break;
                case ItemState.Buy:
                    M_ClothGizmo[i].overrideSprite = M_GizmoBuy;
                    break;
                case ItemState.Equip:
                    M_ClothGizmo[i].overrideSprite = M_GizmoEquip;
                    break;
            }
        }
    }

    public void NormalFootball()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 0;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_selectIndex).M_Material;
        UpdateFootballBuyButton(m_selectIndex);
    }
    public void FireFootball()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 1;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_selectIndex).M_Material;
        UpdateFootballBuyButton(m_selectIndex);
    }
    public void ColorFootball()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Dress);
        m_selectIndex = 2;
        M_Ball.material = Game.M_Instance.M_StaticData.GetFootballInfo(m_selectIndex).M_Material;
        UpdateFootballBuyButton(m_selectIndex);
    }
    public void UpdateFootballBuyButton(int i)
    {
        state = m_gm.CheckFootballState(i);
        switch (state)
        {
            case ItemState.UnBuy:
                M_ImageBuyFootball.gameObject.SetActive(true);
                M_ImageBuyFootball.overrideSprite = M_SpBuy;
                break;
            case ItemState.Buy:
                M_ImageBuyFootball.gameObject.SetActive(true);
                M_ImageBuyFootball.overrideSprite = M_SpEquip;
                break;
            case ItemState.Equip:
                M_ImageBuyFootball.gameObject.SetActive(false);
                break;
        }
    }
    public void OnBuyFootballClick()
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Button);
        state = m_gm.CheckFootballState(m_selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                int money = Game.M_Instance.M_StaticData.GetFootballInfo(m_selectIndex).M_Coin;
                BuySkinClothFootballArgs e = new BuySkinClothFootballArgs()
                {
                    M_Coin = money,
                    M_ID = m_selectIndex
                };
                MVC.SendEvent(Consts.E_BuyFootballController, e);
                break;
            case ItemState.Buy:
                MVC.SendEvent(Consts.E_EquipFootballController, m_selectIndex);
                break;
        }
        UpdateFootballBuyButton(m_selectIndex);
        UpdateFootballGizmo();
    }
    public void UpdateFootballGizmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = m_gm.CheckFootballState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    M_FootballGizmo[i].overrideSprite = M_GizmoUnBuy;
                    break;
                case ItemState.Buy:
                    M_FootballGizmo[i].overrideSprite = M_GizmoBuy;
                    break;
                case ItemState.Equip:
                    M_FootballGizmo[i].overrideSprite = M_GizmoEquip;
                    break;
            }
        }
    }
}
