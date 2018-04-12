using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyToolsArgs
{
    private ItemKind m_itemKind;
    public ItemKind M_ItemKind
    {
        get { return m_itemKind; }
        set { m_itemKind = value; }
    }
    private int m_coin;
    public int M_Coin
    {
        get { return m_coin; }
        set { m_coin = value; }
    }
}
