using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level
{
    ChuchuJelly ball2;

    //override of start
    protected override void Start()
    {
        ball2 = FindObjectOfType<ChuchuJelly>();
        Screen.SetResolution(640, 960, true);

        if (Time.timeScale != 1)
            Time.timeScale = 1;

        paddlePosition = paddle.transform.position;
        ballPosition = ball.transform.position;
        audioSource = GetComponent<AudioSource>();

        firstPaddlePosition = paddle.transform.position;
        firstBallPosition = ball2.transform.position;
    }

    //override of reset
    public override void Reset()
    {
        stopwatchHasStarted = false;
        stopwatch.Reset();
        ball2.transform.position = firstBallPosition;
        paddle.transform.position = firstPaddlePosition;
        ball2.Reset();
    }

    public override void Lose()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

            audioSource.PlayOneShot(loseSound);

        Tools.Wait((int)loseSound.length * 1000 + 500);

        lives--;

        if (lives >= 0)
        {
            Reset();
        }

        ShowProgress(lives < 0);
    }
}
