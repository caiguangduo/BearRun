using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange : MonoBehaviour
{
    private GameObject m_roadNow;
    private GameObject m_roadNext;
    private Transform m_parent;

    private void Start()
    {
        if (m_parent == null)
        {
            m_parent = new GameObject().transform;
            m_parent.position = Vector3.zero;
            m_parent.name = "Road";
        }

        m_roadNow = Game.M_Instance.M_ObjectPool.Spawn(Consts.R_Road1);
        m_roadNow.transform.SetParent(m_parent, true);
        m_roadNext = Game.M_Instance.M_ObjectPool.Spawn(Consts.R_Road2);
        m_roadNext.transform.SetParent(m_parent, true);
        m_roadNext.transform.position += new Vector3(0, 0, 160);

        AddItem(m_roadNow);
        AddItem(m_roadNext);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.Tag_Road)
        {
            Game.M_Instance.M_ObjectPool.UnSpawn(other.gameObject);
            SpawnNewRoad();
        }
    }

    private void SpawnNewRoad()
    {
        int i = Random.Range(1, 5);
        m_roadNow = m_roadNext;
        m_roadNext = Game.M_Instance.M_ObjectPool.Spawn("Pattern_" + i.ToString());
        m_roadNext.transform.SetParent(m_parent, true);
        m_roadNext.transform.position = m_roadNow.transform.position + new Vector3(0, 0, 160);

        AddItem(m_roadNext);
    }

    public void AddItem(GameObject targetRoad)
    {
        var itemChild = targetRoad.transform.Find("Item");
        if (itemChild != null)
        {
            PatternManager patternManager = PatternManager.M_Instance;
            if (patternManager != null && patternManager.m_patterns != null && patternManager.m_patterns.Count > 0)
            {
                Pattern pattern = patternManager.m_patterns[Random.Range(0, patternManager.m_patterns.Count)];
                if (pattern != null && pattern.m_patternItems != null && pattern.m_patternItems.Count > 0)
                {
                    foreach (PatternItem item in pattern.m_patternItems)
                    {
                        GameObject go = Game.M_Instance.M_ObjectPool.Spawn(item.m_prefabName);
                        go.transform.SetParent(itemChild, true);
                        go.transform.localPosition = item.m_pos;
                    }
                }
            }
        }
    }
}
