using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(0, 0, -0.27f);
        transform.localScale = 3.33f * Vector3.one;
        base.OnSpawn();
    }
}
