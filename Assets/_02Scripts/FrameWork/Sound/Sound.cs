using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoSingleton<Sound>
{
    public string M_ResourceDir = "";
    AudioSource m_bg;
    AudioSource m_effect;

    protected override void Awake()
    {
        base.Awake();
        m_bg = gameObject.AddComponent<AudioSource>();
        m_bg.playOnAwake = false;
        m_bg.loop = true;

        m_effect = gameObject.AddComponent<AudioSource>();
        m_effect.playOnAwake = false;
        m_effect.loop = false;
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clipName">想要播放的背景音乐的名字</param>
    public void PlayBG(string clipName)
    {
        if (m_bg.clip != null)
            if (m_bg.clip.name == clipName)
                return;

        string path = M_ResourceDir + "/" + clipName;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            m_bg.clip = clip;
            m_bg.Play();
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clipName">音效名字</param>
    public void PlayEffect(string clipName)
    {
        string path = M_ResourceDir + "/" + clipName;
        AudioClip clip = Resources.Load<AudioClip>(path);
        m_effect.PlayOneShot(clip);
    }
}
