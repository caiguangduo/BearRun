using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ReusableObject
{
    private float m_time = 1f;
    public float M_Time
    {
        get
        {
            return m_time;
        }
        set
        {
            m_time = value;
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(M_Time);
        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }

    public override void OnSpawn()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }
}
