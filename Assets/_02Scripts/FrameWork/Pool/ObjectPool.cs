using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    public string M_ResourceDir = "";
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    /// <summary>
    /// 生成目标游戏物体
    /// </summary>
    /// <param name="name">目标游戏物体的名字</param>
    /// <param name="parent">目标游戏物体在场景中的父物体</param>
    /// <returns></returns>
    public GameObject Spawn(string name)
    {
        SubPool pool = null;
        if (!m_pools.ContainsKey(name))
        {
            RegisterNew(name);
        }
        pool = m_pools[name];
        return pool.Spawn();
    }
    /// <summary>
    /// 如果总对象池中没有包含目标游戏物体的子对象池，为总对象池添加相应的子对象池
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    void RegisterNew(string name)
    {
        string path = M_ResourceDir + "/" + name;
        GameObject go = Resources.Load<GameObject>(path);
        SubPool pool = new SubPool(go);
        m_pools.Add(pool.M_Name, pool);
    }

    /// <summary>
    /// 为对象池回收目标游戏物体
    /// </summary>
    /// <param name="go"></param>
    public void UnSpawn(GameObject go)
    {
        foreach (SubPool p in m_pools.Values)
        {
            if (p.Contain(go))
            {
                p.UnSpawn(go);
                break;
            }
        }
    }

    public void UnSpawnAll()
    {
        foreach (SubPool p in m_pools.Values)
        {
            p.UnSpawnAll();
        }
    }

    public void Clear()
    {
        //foreach (SubPool p in m_pools.Values)
        //{
        //    p.DestroyAll();
        //}
        m_pools.Clear();
    }
}
