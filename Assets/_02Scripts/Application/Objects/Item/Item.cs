using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ReusableObject
{
    protected float m_speed = 60;

    public override void OnSpawn()
    {
    }

    public override void OnUnSpawn()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    private void Update()
    {
        transform.Rotate(0, m_speed * Time.deltaTime, 0);
    }

    public virtual void HitPlayer(Transform pos)
    {

    }
}
