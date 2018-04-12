using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : MonoSingleton<Game>
{
    [HideInInspector]
    public ObjectPool M_ObjectPool;
    [HideInInspector]
    public Sound M_Sound;
    [HideInInspector]
    public StaticData M_StaticData;
    [HideInInspector]
    public GameModel M_GM;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        M_ObjectPool = ObjectPool.M_Instance;
        M_Sound = Sound.M_Instance;
        M_StaticData = StaticData.M_Instance;

        SceneManager.sceneLoaded += LevelLoadedEvent;

        MVC.RegisterModel(new GameModel());
        M_GM = MVC.GetModel<GameModel>();
        M_GM.Init();

        MVC.RegisterController(Consts.E_StartUpController, typeof(StartUpController));
        MVC.SendEvent(Consts.E_StartUpController);

        Game.M_Instance.LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        ScenesArgs e = new ScenesArgs() { M_SceneIndex = SceneManager.GetActiveScene().buildIndex };

        MVC.SendEvent(Consts.E_ExitSceneController, e);

        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    Debug.Log("进入新场景:" + level);

    //    ScenesArgs e = new ScenesArgs() { M_SceneIndex = level };
    //    SendEvent(Consts.E_EnterScenes, e);
    //}
    private void LevelLoadedEvent(Scene scene,LoadSceneMode mode)
    {
        ScenesArgs e = new ScenesArgs() { M_SceneIndex = scene.buildIndex };
        MVC.SendEvent(Consts.E_EnterSceneController, e);
    }
}
