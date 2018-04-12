using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : Effect
{
    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(0, 13.73f, -2.89f);
        transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);
        base.OnSpawn();
    }
}
