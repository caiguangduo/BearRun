using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.M_SceneIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Game.M_Instance.M_ObjectPool.Clear();
                break;
        }

        Game.M_Instance.M_GM.M_LastIndex = e.M_SceneIndex;
    }
}
