using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Obstacles
{
    private Animation m_animation;
    private Animation M_Animation
    {
        get
        {
            if (m_animation == null)
            {
                m_animation = GetComponentInChildren<Animation>();
            }
            return m_animation;
        }
    }
    
    private float m_speed = 10;

    public override void HitPlayer(Vector3 pos)
    {
        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(Consts.O_FX_ZhuangJi);
        go.transform.SetParent(M_EffectParent, true);
        go.transform.position = pos;

        M_Animation.Play("fly");
        StopAllCoroutines();
        StartCoroutine(StartFly());
    }
    IEnumerator StartFly()
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                transform.position += new Vector3(0, m_speed, m_speed) * Time.deltaTime;
            }
            yield return null;
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        M_Animation.Play("run");
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        M_Animation.transform.localPosition = Vector3.zero;
        StopAllCoroutines();
    }

    public void HitTrigger()
    {
        StartCoroutine(StartXMove());
    }
    IEnumerator StartXMove()
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                transform.position -= new Vector3(m_speed, 0, 0) * Time.deltaTime;
            }
            yield return null;
        }
    }
}
