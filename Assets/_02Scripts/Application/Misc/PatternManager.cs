using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatternManager : MonoSingleton<PatternManager>
{
    public List<Pattern> m_patterns = new List<Pattern>();
}

[Serializable]
public class PatternItem
{
    public string m_prefabName;
    public Vector3 m_pos;
}
[Serializable]
public class Pattern
{
    public List<PatternItem> m_patternItems = new List<PatternItem>();
}

