using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseArgs
{
    private int m_coin;
    public int M_Coin
    {
        get { return m_coin; }
        set { m_coin = value; }
    }
    private int m_score;
    public int M_Score
    {
        get { return m_score; }
        set { m_score = value; }
    }
    private int m_distance;
    public int M_Distance
    {
        get { return m_distance; }
        set { m_distance = value; }
    }
}
