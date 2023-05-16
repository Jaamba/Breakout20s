using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockTypes
{
    diamondBlock,
    stoneBlock,
    leavesBlock,
    grassBlock,
    woodBlock
}

public class Level1Block : Block
{
    //config params
    [SerializeField] public BlockTypes blockType;
    
    //level1 param actually corresponds to base level param
    private Level1 level1;

    //Start is called before the first frame update
    protected override void Start()
    {
        level = FindObjectOfType<Level1>();
        level1 = FindObjectOfType<Level1>();

        level.AddBlock();
        level1.IncrementBlockOfType(blockType);
    }

    //when a type of block is destroyed
    public override void Break()
    {
        if (blockType == BlockTypes.leavesBlock)
            BlockState = BlockState.destroyed;

        if (BlockState != BlockState.destroyed)
            BlockState++;
    }

    //checks if has to destroy the block
    protected override void CheckIfDestroy()
    {
        if(BlockState == BlockState.destroyed)
        {
            Destroy(gameObject);
            level.RemoveBlock();
            level.AddPoints(pointsToAdd);
            level1.DecrementBlockOfType(blockType);
        }
    }

}
