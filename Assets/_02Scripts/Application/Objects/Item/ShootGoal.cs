using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ReusableObject
{
    public Animation m_goalKeeper;
    public Animation m_door;
    public GameObject m_net;
    public float m_speed = 10;

    public override void OnSpawn()
    {
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();

        m_net.SetActive(true);
        m_goalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(true);
        m_goalKeeper.Play("standard");
        m_door.Play("QiuMen_St");

        m_goalKeeper.transform.localPosition = Vector3.zero;
    }

    public void ShootAGoal(int i)
    {
        switch (i)
        {
            case -2:
                m_goalKeeper.Play("left_flutter");
                break;
            case 0:
                m_goalKeeper.Play("flutter");
                break;
            case 2:
                m_goalKeeper.Play("right_flutter");
                break;
        }
        StartCoroutine(HideGoalKeeper());
    }
    IEnumerator HideGoalKeeper()
    {
        yield return new WaitForSeconds(0.65f);
        m_goalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

    public void HitGoalKeeper()
    {
        m_goalKeeper.Play("fly");
        StartCoroutine(GoalKeeperFly());
    }
    IEnumerator GoalKeeperFly()
    {
        while (true)
        {
            m_goalKeeper.transform.position += new Vector3(0, m_speed, m_speed) * Time.deltaTime;
            yield return null;
        }
    }

    public void HitDoor(int i)
    {
        switch (i)
        {
            case 0:
                m_door.Play("QiuMen_RR");
                break;
            case 1:
                m_door.Play("QiuMen_St");
                break;
            case 2:
                m_door.Play("QiuMen_LR");
                break;
        }
        m_net.SetActive(false);
    }
}
