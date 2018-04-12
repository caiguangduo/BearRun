using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Item
{
    public override void HitPlayer(Transform pos)
    {
        Game.M_Instance.M_Sound.PlayEffect(Consts.S_Se_UI_Magnet);
        Game.M_Instance.M_ObjectPool.UnSpawn(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Tag_Player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitItem", ItemKind.ItemMagnet);
        }
    }
}
