using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private Transform m_effectParent;
    private Transform M_EffectParent
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

    private float m_moveSpeed = 40;

    public override void HitPlayer(Transform pos)
    {
        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(Consts.O_FX_JinBi);
        go.transform.SetParent(M_EffectParent,true);
        go.transform.position = pos.position;

        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_JinBi);

        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Tag_Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
        else if (other.tag==Tag.Tag_MagnetCollider)
        {
            StartCoroutine(HitMagnet(other.transform));
        }
    }
    IEnumerator HitMagnet(Transform pos)
    {
        bool isLoop = true;
        while (isLoop)
        {
            transform.position = Vector3.Lerp(transform.position, pos.position, m_moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pos.position) < 0.3f)
            {
                isLoop = false;
                HitPlayer(pos.parent);
                pos.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }
            yield return null;
        }
    }
}
