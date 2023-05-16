using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    normal,
    broken,
    reallyBroken,
    destroyed
}

public class Block : MonoBehaviour
{
    //config params.
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected bool playSound;
    [SerializeField] protected int pointsToAdd;
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected bool isUnbreakable;

    //params.
    protected Level level;
    public BlockState BlockState { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        level = FindObjectOfType<Level>();
        level.AddBlock();
        BlockState = BlockState.normal;
    }
    
    //checks if he has to change the sprite
    protected virtual void Update()
    {
        if (!isUnbreakable)
        {
            if (sprites.Length == 3)
            {
                switch (BlockState)
                {
                    case BlockState.normal:
                        GetComponent<SpriteRenderer>().sprite = sprites[(int)BlockState];
                        break;

                    case BlockState.broken:
                        GetComponent<SpriteRenderer>().sprite = sprites[(int)BlockState];
                        break;

                    case BlockState.reallyBroken:
                        GetComponent<SpriteRenderer>().sprite = sprites[(int)BlockState];
                        break;
                }
            }

            CheckIfDestroy();
        }
    }

    //destory the bock when the ball colides with it and plays a sound.
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        //(Sometimes start process may not work propely)
        if (level == null)
            level = FindObjectOfType<Level>();

        if (playSound)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        if (!isUnbreakable)
        {
            Break();
        }
    }

    //reduces by 1 block state
    virtual public void Break()
    {
        if (BlockState != BlockState.destroyed)
            BlockState++;
    }

    //checks if has to destroy the block
    virtual protected void CheckIfDestroy() 
    {
        if(BlockState == BlockState.destroyed)
        {
            Destroy(gameObject);
            level.RemoveBlock();
            level.AddPoints(pointsToAdd);
        }
    }

    

}
