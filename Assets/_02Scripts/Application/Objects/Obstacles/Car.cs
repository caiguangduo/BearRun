using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacles
{
    [SerializeField]
    private bool m_canMove = false;

    private float m_speed = 10;

    public override void HitPlayer(Vector3 pos)
    {
        base.HitPlayer(pos);
    }
    public override void OnSpawn()
    {
    }
    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }

    public void HitTrigger()
    {
        if (m_canMove)
            StartCoroutine(MoveCar());
    }

    IEnumerator MoveCar()
    {
        while (true)
        {
            if (!Game.M_Instance.M_GM.M_IsPause && Game.M_Instance.M_GM.M_IsPlay)
            {
                transform.Translate(-transform.forward * m_speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
