using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool 
{
    List<GameObject> m_objects = new List<GameObject>();
    GameObject m_prefab;
   
    public string M_Name
    {
        get { return m_prefab.name; }
    }

    public SubPool(GameObject prefab)
    {
        m_prefab = prefab;
    } 

    public GameObject Spawn()
    {
        GameObject go = null;
        foreach (GameObject obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
                go.SetActive(true);
                break;
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            if (!go.activeSelf)//防止在制作预制物体时，将预制物体的enable属性置为false
                go.SetActive(true);
            m_objects.Add(go);
        }

        //注意，虽然上述代码段通过go.SetActive(true);将go设置为true，但其go.activeInHierarchy属性依然为false，随意无法在这里执行go.SendMessage("OnSpawn", SendMessageOptions.RequireReceiver);
        //go.SendMessage("OnSpawn", SendMessageOptions.RequireReceiver);

        go.GetComponent<ReusableObject>().OnSpawn();

        return go;
    }

    public void UnSpawn(GameObject go)
    {
        //go.SendMessage("OnUnSpawn", SendMessageOptions.RequireReceiver);
        go.GetComponent<ReusableObject>().OnUnSpawn();
        go.SetActive(false);
    }

    public void UnSpawnAll()
    {
        foreach (GameObject obj in m_objects)
        {
            if (obj.activeSelf)
            {
                UnSpawn(obj);
            }
        }
    }

    //public void DestroyAll()
    //{
    //    int count = m_objects.Count;
    //    for (int i = count-1; i >=0; i++)
    //    {
    //        GameObject.Destroy(m_objects[i]);
    //    }
    //}

    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
