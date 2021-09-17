using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void toPlayScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1); 
    }

    public void toCredits()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void toMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
