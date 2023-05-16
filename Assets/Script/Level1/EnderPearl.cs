using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderPearl : Ball
{
    //override of update
    protected override void Update()
    {
        base.Update();
    }

    //checks if the collider is not a  block, then plays the sound.
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (playSound && hasLaunched && clips.Length != 0)
        {
            if (!collision.gameObject.TryGetComponent<Block>(out Block block))
                audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
        }

        AddRandomDirection();
    }

    //resets the ball
    public override void Reset()
    {
        hasLaunched = false;
        CanPlayRallenty = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.ballAndPaddleOffset = CalculateBallAndPaddleOffset();
    }

    //makes sure that the ball is going to beat the last block and actually plays the rallenty.
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanPlayRallenty)
        {
            if(collider.name.Contains("Diamond"))
            {
                Time.timeScale = rallentySpeed;
            }
        }
        
    }

    public override void AdjustVelocity()
    {
        if (Math.Abs(GetComponent<Rigidbody2D>().velocity.x) + Math.Abs(GetComponent<Rigidbody2D>().velocity.y) != totalVelcity)
        {
            GetComponent<Rigidbody2D>().velocity = Tools.RestrictVector(GetComponent<Rigidbody2D>().velocity, totalVelcity);
        }
    }
}
