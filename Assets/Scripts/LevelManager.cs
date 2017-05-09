using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("How long does it take to load the next level, leave at 0 to avoid loading the next level.")]
    public float autoLoadNextLevelAfter;

    //void Awake()
    //{
    //    DontDestroyOnLoad(transform.gameObject);
    //}

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)
        {
            Debug.Log("Auto Load Disabled, or use a positive number.");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        }
    }

    public void LoadLevel(string name)
    {
        //Debug.Log("Level Loaded Requested for " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit Requested.");
        Application.Quit();
    }

    public void LoadNextLevel(string name)
    {
        SceneManager.LoadScene(name);
        //SceneManager.LoadScene(SceneManager.loadedlevel + 1);
    }
}