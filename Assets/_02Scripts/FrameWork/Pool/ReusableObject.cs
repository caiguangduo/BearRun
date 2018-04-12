using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour
{
    /// <summary>
    /// 取出对象池中游戏物体时调用
    /// </summary>
    public abstract void OnSpawn();

    /// <summary>
    /// 回收对象池中游戏物体时调用
    /// </summary>
    public abstract void OnUnSpawn();
}
