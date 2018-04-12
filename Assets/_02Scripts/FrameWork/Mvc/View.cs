using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string M_Name { get; }

    [HideInInspector]
    public List<string> M_AttentionList = new List<string>();

    public abstract void RegisterAttentionEvent();

    public abstract void HandleEvent(string name, object data);
}
