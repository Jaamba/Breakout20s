using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Level2LoseTrigger : MonoBehaviour
{
    //params
    Level level;
    public bool AlreadyPlayed { get; set; } = false;
    private Stopwatch stopwatch;

    //Start is called before the first frame update
    public void Start()
    {
        stopwatch = new Stopwatch();
        level = FindObjectOfType<Level>();
    }

    private void Update()
    {
        if (stopwatch.ElapsedMilliseconds > 5000)
        {
            stopwatch.Stop();
            stopwatch.Reset();
            AlreadyPlayed = false;
        }
    }

    //When you lose the game.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!AlreadyPlayed)
        {
            stopwatch.Start();
            AlreadyPlayed = true;

            if (collision == null)
                throw new UnityException("The collider cannot be null");

            if (collision.tag.Equals("ball"))
            {
                if (level == null)
                {
                    level = FindObjectOfType<Level>();
                }
                level.Lose();
            }
        }
    }
}
