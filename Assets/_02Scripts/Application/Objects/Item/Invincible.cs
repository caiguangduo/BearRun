using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Tag_Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitItem", ItemKind.ItemInvincible);
        }
    }
    public override void HitPlayer(Transform pos)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Whist);
        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }
}
