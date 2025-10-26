using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        HeroControl.sceneCounter = 0;
        HeroControl.attemptCounter = 1;
        HeroControl.score = 0;

//        QualitySettings.vSyncCount = 1;
//        Application.targetFrameRate = 60;

    }


    public void QuitGame()
    {
        LoadSituations.CloseLog();
        Application.Quit();
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }
}
