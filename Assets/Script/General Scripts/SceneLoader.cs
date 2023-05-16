using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//Scene loader with static methods
public class GlobalSceneLoader : MonoBehaviour
{
    static public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    static public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    
    static public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    static public void QuitGame()
    {
        Application.Quit();
    }

    public static void LoadSceneByString(string name)
    {
        if (name == null)
            throw new UnityException("The name cannot be null", new ArgumentNullException(nameof(name)));

        SceneManager.LoadScene(name);
    }

    static public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

//*For practice in use, either scene loader and global scene loader are written in the same file

//Scene loader with non static methods
public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneByString(string name)
    {
        if (name == null)
            throw new UnityException("The name cannot be null", new ArgumentNullException(nameof(name)));

        SceneManager.LoadScene(name);
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
