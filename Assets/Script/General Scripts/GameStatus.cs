using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    //fields
    public bool[] LevelsCompleted { get; set; }
    public bool[] OpenLocks { get; set; } 

    [SerializeField] int levelsNum;

    //makes sure that GameStatus is a singleton pattern script
    private void Awake()
    {
        if(FindObjectsOfType<GameStatus>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        LevelsCompleted = new bool[levelsNum];
        OpenLocks = new bool[levelsNum];
    }

}
