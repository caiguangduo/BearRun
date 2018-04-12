using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(-0.4f, 0, -3.56f);
        transform.localScale = 1.67f * Vector3.one;
        base.OnSpawn();
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
