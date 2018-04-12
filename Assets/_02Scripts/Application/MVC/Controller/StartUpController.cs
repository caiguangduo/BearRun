using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpController : Controller
{
    public override void Execute(object data)
    {
        MVC.RegisterController(Consts.E_EnterSceneController, typeof(EnterSceneController));
        MVC.RegisterController(Consts.E_ExitSceneController, typeof(ExitSceneController));


        MVC.RegisterController(Consts.E_EndGameController, typeof(EndGameController));
        MVC.RegisterController(Consts.E_ResumeGameController, typeof(ResumeGameController));
        MVC.RegisterController(Consts.E_ContinueGameController, typeof(ContinueGameController));
        MVC.RegisterController(Consts.E_PauseGameController, typeof(PauseGameController));
        MVC.RegisterController(Consts.E_FinalShowUIController, typeof(FinalShowUIController));
        MVC.RegisterController(Consts.E_BriberyClickController, typeof(BriberyClickController));

        MVC.RegisterController(Consts.E_HitItemController, typeof(HitItemController));

        MVC.RegisterController(Consts.E_BuyToolsController, typeof(BuyToolsController));

        MVC.RegisterController(Consts.E_BuySkinController, typeof(BuySkinController));
        MVC.RegisterController(Consts.E_BuyClothController, typeof(BuyClothController));
        MVC.RegisterController(Consts.E_BuyFootballController, typeof(BuyFootballController));

        MVC.RegisterController(Consts.E_EquipSkinController, typeof(EquipSkinController));
        MVC.RegisterController(Consts.E_EquipClothController, typeof(EquipClothController));
        MVC.RegisterController(Consts.E_EquipFootballController, typeof(EquipFootballController));

    }
}
