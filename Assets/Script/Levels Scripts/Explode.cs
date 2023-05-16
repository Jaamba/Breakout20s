using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Explode : MonoBehaviour
{
    [SerializeField] Level level;
    [SerializeField] int pointsNeededToExpolde;

    int timesExploded = 1;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        CheckAndExplode();
    }

    //checks if can explodes and then does it
    public void CheckAndExplode()
    {
        int explosionPoints = (int)(pointsNeededToExpolde * timesExploded * 0.7 /*arbitrary value*/);

        if (level.Score >= explosionPoints) 
        {
            text.text = "press e to explode!";

            if(Input.GetKeyDown(KeyCode.E))
            {
                level.Explode();
                level.Score -= explosionPoints;
                timesExploded++;

            }
        }
        else
        {
            text.text = "";
        }

    }
}
