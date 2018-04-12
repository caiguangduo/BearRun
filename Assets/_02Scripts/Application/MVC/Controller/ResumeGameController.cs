using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameController : Controller
{
    public override void Execute(object data)
    {
        UIResume resume = MVC.GetView<UIResume>();
        resume.StartCount();
    }
}
