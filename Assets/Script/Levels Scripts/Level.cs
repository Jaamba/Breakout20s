using System.Collections;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    //config params.
    [SerializeField] protected Paddle paddle;
    [SerializeField] protected Ball ball;
    [SerializeField] protected bool showOnConsole;
    [SerializeField] protected AudioClip winSound;
    [SerializeField] protected AudioClip loseSound;
    [SerializeField] protected bool playRallenty;
    [SerializeField] protected int pointsPerBlock;
    [SerializeField] protected int LevelNum;
    [SerializeField] protected int lives;
    [SerializeField] protected LevelCanvas canvas;
    [SerializeField] protected Block[] blocks;
    [SerializeField] protected int ExplodeDivisor;
    [SerializeField] protected AudioClip explosionSound;

    //params.
    public int BlocksCount { get; protected set; } = 0;
    protected Vector2 paddlePosition;
    protected Vector2 ballPosition;
    public bool HasWon { get; private set; } = false;
    protected AudioSource audioSource;
    protected Stopwatch stopwatch = new Stopwatch();
    protected bool stopwatchHasStarted = false;
    protected int previousBlocks = 0;
    public int Score { get; set; } = 0;
    protected Vector2 firstPaddlePosition, firstBallPosition;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Screen.SetResolution(640, 960, true);

        if (Time.timeScale != 1)
            Time.timeScale = 1;

        paddlePosition = paddle.transform.position;
        ballPosition = ball.transform.position;
        audioSource = GetComponent<AudioSource>();

        firstPaddlePosition = paddle.transform.position;
        firstBallPosition = ball.transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        //updates ball and paddle positions
        paddlePosition = paddle.transform.position;
        ballPosition = ball.transform.position;

        //shows the content on the console
        if (showOnConsole)
        {
            ShowDetailsOnConsole();
        }

        CheckIfHasWon();
        CheckIfCanPlayRallenty();
        PrintScore();
    }

    //checks if the player has won
    protected virtual void CheckIfHasWon()
    {
        if (BlocksCount <= 0)
        {
            if (!stopwatchHasStarted)
            {
                stopwatch.Start();
                stopwatchHasStarted = true;
            }

            Tools.WaitAndDo(500, Win, stopwatch, true);
        }
    }

    //checks if has to play the rallenty
    protected virtual void CheckIfCanPlayRallenty()
    {
        if (playRallenty && BlocksCount == 1)
        {
            ball.CanPlayRallenty = true;
        }
    }

    //shows level details on the consolle.
    public virtual void ShowDetailsOnConsole()
    {
        print("Blocks count: " + BlocksCount);
        print("paddle position: " + paddlePosition);
        print("ball position: " + ballPosition);
    }

    //notices that a block has been added.
    public void AddBlock()
    {
        BlocksCount++;
    }

    //notices that a block has been destroyed;
    public void RemoveBlock()
    {
        if(BlocksCount > 0)
        {
            BlocksCount--;
        } 
    }

    //wins the level.
    public virtual void Win()
    {
        HasWon = true;
        audioSource.PlayOneShot(winSound);

        Tools.Wait( (int) winSound.length * 1000 + 500);

        GlobalSceneLoader.LoadSceneByString("SelectLevel");

        FindObjectOfType<GameStatus>().LevelsCompleted[this.LevelNum] = true;
    }

    //loses the game
    public virtual void Lose()
    {
        if(audioSource == null)
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

    //adds some points when a block is destroyed
    public void AddPoints(int points)
    {
        Score += points;
    }

    //prints the score
    public void PrintScore()
    {
        canvas.SetScore(Score);
    }

    //resets the level
    public virtual void Reset()
    {
        stopwatchHasStarted = false;
        stopwatch.Reset();
        ball.transform.position = firstBallPosition;
        paddle.transform.position = firstPaddlePosition;
        ball.Reset();
    }

    //shows what you did in the level after losing
    public void ShowProgress(bool hasLost)
    {
        if (!hasLost)
        {
            canvas.SetBlur();
            canvas.SetProgress();
            canvas.SetScoreProgress(Score);
            canvas.SetLevel(LevelNum + 1);
            canvas.SetLives(lives + 1);
        }

        if (hasLost)
        {
            Tools.Wait(1000);
            GlobalSceneLoader.LoadSceneByIndex(1);
        }
    }

    //explodes
    public virtual void Explode()
    {
        int blocksToDestroy = UnityEngine.Random.Range(1, blocks.Length) / ExplodeDivisor;

        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1);

        int counter = 0;
        foreach(Block block in blocks)
        {
            if (counter < blocksToDestroy)
            {
                if (Tools.RandomBool() && block != null)
                {
                    block.Break();
                    counter++;
                }
            }
        }
    }
}
