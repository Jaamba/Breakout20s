using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    //params.
    public int diamondBlocks { get; private set; }
    public int stoneBlocks { get; private set; } 
    public int grassBlocks { get; private set; }
    public int leavesBlocks { get; private set; }
    public int woodBlocks { get; private set; }
    
    [SerializeField] public Level1Block[] level1Blocks;

    private EnderPearl enderPearl;

    //Start is called before the first frame
    protected override void Start()
    {
        base.Start();
        enderPearl = FindObjectOfType<EnderPearl>();
    }

    //resets the level
    public override void Reset()
    {
        stopwatchHasStarted = false;
        stopwatch.Reset();
        paddle.transform.position = firstPaddlePosition;
        enderPearl.transform.position = firstBallPosition;
        enderPearl.Reset();
    }

    //checks if the player has won
    protected override void CheckIfHasWon()
    {
        if(diamondBlocks <= 0)
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
    protected override void CheckIfCanPlayRallenty()
    {
        if (playRallenty && diamondBlocks == 1)
        {
            enderPearl.CanPlayRallenty = true;
        }
    }

    //increments the number of blocks of a certain type
    public void IncrementBlockOfType(BlockTypes block)
    {
        if(block == BlockTypes.diamondBlock)
        {
            diamondBlocks++;
        }
        else if(block == BlockTypes.stoneBlock)
        {
            stoneBlocks++;
        }
        else if (block == BlockTypes.grassBlock)
        {
            grassBlocks++;
        }
        else if (block == BlockTypes.leavesBlock)
        {
            leavesBlocks++;
        }
        else if (block == BlockTypes.woodBlock)
        {
            woodBlocks++;
        }
    }

    //decrements a block of a certain type
    public void DecrementBlockOfType(BlockTypes block)
    {
        if (block == BlockTypes.diamondBlock)
        {
            diamondBlocks--;
        }
        else if (block == BlockTypes.stoneBlock)
        {
            stoneBlocks--;
        }
        else if (block == BlockTypes.grassBlock)
        {
            grassBlocks--;
        }
        else if (block == BlockTypes.leavesBlock)
        {
            leavesBlocks--;
        }
        else if (block == BlockTypes.woodBlock)
        {
            woodBlocks--;
        }
    }

    //shows the details on the console
    public override void ShowDetailsOnConsole()
    {
        base.ShowDetailsOnConsole();

        print("diamonds blocks: " + diamondBlocks + '\n' +
              "stone blocks: " + stoneBlocks + '\n' +
              "leaeves blocks: " + leavesBlocks + '\n' +
              "grass blocks: " + grassBlocks + '\n' +
              "wood blocks: " + woodBlocks);
    }

    //explodes
    public override void Explode()
    {
        int blocksToDestroy = UnityEngine.Random.Range(1, level1Blocks.Length) / ExplodeDivisor;

        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1);

        int counter = 0;
        foreach (Level1Block block in level1Blocks)
        {
            if (counter < blocksToDestroy)
            {
                if (Tools.RandomBool() && block != null)
                {
                    if (block.blockType != BlockTypes.diamondBlock)
                    {
                        block.Break();
                        counter++;
                    }
                }
            }
        }
    }
}
