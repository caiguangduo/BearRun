using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : ReusableObject
{
    protected Transform m_effectParent;
    protected Transform M_EffectParent
    {
        get
        {
            if (m_effectParent == null)
            {
                m_effectParent = GameObject.Find(Consts.O_EffectParent).transform;
            }
            return m_effectParent;
        }
    }

    public virtual void HitPlayer(Vector3 pos)
    {
        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(Consts.O_FX_ZhuangJi);
        go.transform.SetParent(M_EffectParent, true);
        go.transform.position = pos;

        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }

    public override void OnSpawn()
    {
    }

    public override void OnUnSpawn()
    {
    }
}
