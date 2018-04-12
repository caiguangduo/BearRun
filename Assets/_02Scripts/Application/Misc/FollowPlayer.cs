using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform m_player;
    private Transform M_Player
    {
        get
        {
            if (m_player == null)
            {
                m_player = GameObject.FindWithTag(Tag.Tag_Player).transform;
            }
            return m_player;
        }
    }
    private Vector3 m_offset;
    private float m_speed = 20;

    private void Awake()
    {
        m_offset = transform.position - M_Player.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_offset + M_Player.position, m_speed * Time.deltaTime);
    }
}
