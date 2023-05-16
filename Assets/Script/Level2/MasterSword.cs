using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordState
{
    basic,
    moved,
    movedTwice,
    movedThree,
    movedFour,
    movedFive,
    movedSix,
    movedSeven,
    movedEight,
    movedOut,
    finished
}

public class MasterSword : MonoBehaviour
{
    private SwordState state;
    private bool hasMove = false;
    private bool hasMoveOut = false;
    private float firstY;
    private float points = 0;

    [SerializeField] float changeAdd;
    [SerializeField] float movingDivisor;
    [SerializeField] float changeAddOut;
    [SerializeField] float movingDivisorOut;
    [SerializeField] int pointsPerMove;
    [SerializeField] int pointsPerWin;
    [SerializeField] Level2 level;


    private void Start()
    {
        state = SwordState.basic;
    }

    private void Update()
    {
        if(hasMove)
        {
            points += pointsPerMove / movingDivisor;
            if(points >= 1)
            {
                level.AddPoints(Convert.ToInt32(points));
                points = 0;
            }

            Move();
        }
        
        if(hasMoveOut)
        {
            points += pointsPerWin / movingDivisorOut;
            if (points >= 1)
            {
                level.AddPoints(Convert.ToInt32(points));
                points = 0;
            }
            MoveOut();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ball") && state != SwordState.movedOut && state != SwordState.finished)
        {
            firstY = transform.position.y;
            hasMove = true;
            state++;
        }
        else if(collision.gameObject.tag.Equals("ball") && state == SwordState.movedOut)
        {
            firstY = transform.position.y;
            hasMoveOut = true;
            state++;
        }
    }

    //moves the sword
    public void Move()
    {
        if(transform.position.y < firstY + changeAdd)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + changeAdd / movingDivisor, 1);
        }
        else
        {
            hasMove = false;
        }


    }

    //wins the level and moves out the sword
    public void MoveOut()
    {
        if (transform.position.y < firstY + changeAddOut)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + changeAdd / movingDivisorOut, 1);
        }
        else if(GetComponent<SpriteRenderer>().material.color.a > 0)
        {
            GetComponent<SpriteRenderer>().material.color = new Color(GetComponent<SpriteRenderer>().material.color.r,
                GetComponent<SpriteRenderer>().material.color.g,
                GetComponent<SpriteRenderer>().material.color.b,
                GetComponent<SpriteRenderer>().material.color.a - 0.01f);
        }
        else
        {
            hasMoveOut = false;
        }

        if(GetComponent<SpriteRenderer>().material.color.a <= 0)
        {
            level.Win();
            hasMoveOut = false;
        }

    }
}
