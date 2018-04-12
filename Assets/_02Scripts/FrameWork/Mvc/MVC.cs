using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MVC
{
    public static Dictionary<string, Model> M_Models = new Dictionary<string, Model>();
    public static Dictionary<string, View> M_Views = new Dictionary<string, View>();
    public static Dictionary<string, Type> M_CommandMap = new Dictionary<string, Type>();

    public static void RegisterView(View view)
    {
        if (M_Views.ContainsKey(view.M_Name))
        {
            M_Views.Remove(view.M_Name);
        }

        view.RegisterAttentionEvent();
        M_Views[view.M_Name] = view;
    }

    public static void RegisterModel(Model model)
    {
        M_Models[model.M_Name] = model;
    }

    public static void RegisterController(string eventName,Type controllerType)
    {
        M_CommandMap[eventName] = controllerType;
    }

    public static T GetModel<T>() where T:Model
    {
        foreach (var m in M_Models.Values)
        {
            if(m is T)
            {
                return (T)m;
            }
        }
        return null;
    }

    public static T GetView<T>() where T:View
    {
        foreach (var v in M_Views.Values)
        {
            if(v is T)
            {
                return (T)v;
            }
        }
        return null;
    }

    public static void SendEvent(string eventName,object data = null)
    {
        if (M_CommandMap.ContainsKey(eventName))
        {
            Type t = M_CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;
            c.Execute(data);
        }
        else
        {
            foreach (var v in M_Views.Values)
            {
                if (v.M_AttentionList.Contains(eventName))
                {
                    v.HandleEvent(eventName, data);
                }
            }
        }
    }
}
