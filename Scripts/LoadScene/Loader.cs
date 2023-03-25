using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene{
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    
    private static Action onLoadercallback;
    public static void Load(Scene targetScene)
    {
        
        onLoadercallback = () =>
        {
            SceneManager.LoadScene(targetScene.ToString());
        };
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }
    public static void LoaderCallback()
    {
        if(onLoadercallback != null)
        {
            onLoadercallback();
            onLoadercallback = null;
        }    
    }
}
