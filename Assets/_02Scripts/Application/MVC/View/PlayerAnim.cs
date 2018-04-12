using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : View
{
    const string m_constRun = "run";
    const string m_constLeftJump = "left_jump";
    const string m_constRightJump = "right_jump";
    const string m_constRoll = "roll";
    const string m_constJump = "jump";
    const string m_constShoot = "Shoot01";

    public override string M_Name
    {
        get
        {
            return Consts.V_PlayerAnim;
        }
    }

    private Animation m_animation;
    private Animation M_Animation
    {
        get
        {
            if (m_animation == null)
            {
                m_animation = GetComponent<Animation>();
            }
            return m_animation;
        }
    }

    private Action m_playAnim;

    private void Awake()
    {
        PlayAnimClip(m_constRun);
    }
  
    void PlayAnimClip(string clip)
    {
        if (!M_Animation.IsPlaying(clip))
        {
            M_Animation.Play(clip);
        }
        if (clip == m_constRun) return;

        StopAllCoroutines();
        StartCoroutine(UpdateAnimState(clip));
    }
    IEnumerator UpdateAnimState(string clip)
    {
        while (true)
        {
            if (M_Animation[clip].normalizedTime > 0.55f)
            {
                PlayAnimClip(m_constRun);
                break;
            }
            yield return null;
        }
    }

    public void MessagePlayShoot()
    {
        PlayAnimClip(m_constShoot);
    } 

    public void AnimManager(InputDirection dir)
    {
        string targetClip = null;
        switch (dir)
        {
            case InputDirection.Right:
                targetClip = m_constRightJump;
                break;
            case InputDirection.Left:
                targetClip = m_constLeftJump;
                break;
            case InputDirection.Down:
                targetClip = m_constRoll;
                break;
            case InputDirection.Up:
                targetClip = m_constJump;
                break;
        }
        PlayAnimClip(targetClip);
    }

    public void StopPlayerAnim()
    {
        M_Animation.Stop();
    }
    public void ContinuePlay()
    {
        M_Animation.Play();
    }

    public override void RegisterAttentionEvent()
    {
    }
    public override void HandleEvent(string name, object data)
    {
    }
}
