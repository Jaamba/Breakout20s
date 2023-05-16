using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChuchuJelly : Ball
{
    //params
    [SerializeField] int rotationsLimit;
    [SerializeField] Sprite arrow;
    [SerializeField] Arrow arrowScript;
    [SerializeField] float stickDelay;
    [SerializeField] AudioClip[] stickClips;
    [SerializeField] float soundDelay;
    [SerializeField] float startAngularVelocity;
    [SerializeField] AudioClip[] leavesSounds;
 
    //fields
    private Rigidbody2D _rigidbody;
    public int TotalVelocity { get; set; }
    public bool StopsMoving { get; set; } = false;
    private bool canStick = true;
    private Stopwatch stopwatch;
    private Stopwatch stopwatch2;
    private bool hasGravity = false;

    protected override void Start()
    {
        base.Start();

        _rigidbody = GetComponent<Rigidbody2D>();
        TotalVelocity = Convert.ToInt32(speedOnLaunch.x + speedOnLaunch.y);
        this.hasLaunched = false;
        this.stopwatch = new Stopwatch();
        this.stopwatch2 = new Stopwatch();
    }

    //resets ball fields
    public override void Reset()
    {
        CanPlayRallenty = false;
        hasLaunched = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.ballAndPaddleOffset = CalculateBallAndPaddleOffset();
        _rigidbody.rotation = 0;
        _rigidbody.angularVelocity = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    //override of update
    protected override void Update()
    {
        if (!hasLaunched)
        {
            Tools.CheckAndDo(KeyCode.Mouse1, Launch);
            transform.position = CalculateBallPositionBeforeLaunch();
        }
        else
        {
            if (Math.Abs(_rigidbody.angularVelocity) > rotationsLimit && !hasGravity)
            {
                if (_rigidbody.angularVelocity > 0)
                {
                    _rigidbody.angularVelocity = rotationsLimit;
                }
                else
                {
                    _rigidbody.angularVelocity = rotationsLimit * -1;
                }
            }

            if (!StopsMoving && !hasGravity)
            {
                this.AdjustVelocity();
            }

            CheckDelay();
        }

        base.CalculateDirection();


        if (StopsMoving)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.angularVelocity = 0;

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _rigidbody.velocity = arrowScript.ballVelocity(1);
                StopsMoving = false;
                StickDelay();

                arrowScript.specialCounter = 0;

                _rigidbody.angularVelocity = startAngularVelocity;
            }
        }
    }

    //adds a delay of x seconds to canStick
    public void StickDelay()
    {
        canStick = false;
        stopwatch.Start();
    }

    //checks the sticks delay
    public void CheckDelay()
    {
        if(stopwatch.ElapsedMilliseconds > stickDelay)
        {
            canStick = true;
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }

    //override of Launch method
    public override void Launch()
    {
        base.Launch();

        _rigidbody.angularVelocity = startAngularVelocity;
    }

    //useless methods in level 2
    protected override void OnTriggerEnter2D(Collider2D collider) {}
    protected override void OnTriggerExit2D(Collider2D collider) { hasLaunched = false; }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        AddRandomDirection();

        //checks what to do on collision enter
        if(hasGravity && !collision.gameObject.tag.Equals("plant"))
        {
            hasGravity = false;
            _rigidbody.gravityScale = 0f;
        }

        if (collision.gameObject.tag.Equals("stickable") && canStick)
            StopsMoving = true;
        
        else if(collision.gameObject.tag.Equals("plant"))
        {
            hasGravity = true;
            _rigidbody.gravityScale = 1f;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0;

        }

        //plays the audio
        if (stopwatch2.ElapsedMilliseconds > soundDelay || stopwatch2.ElapsedMilliseconds == 0)
        {
            if(collision.gameObject.tag.Equals("plant") && hasLaunched && playSound && leavesSounds.Length > 0)
            {
                AudioClip clip = leavesSounds[UnityEngine.Random.Range(0, leavesSounds.Length)];
                audioSource.PlayOneShot(clip);
            }
            else if (hasLaunched && playSound && clips.Length > 0 && StopsMoving)
            {
                AudioClip clip = stickClips[UnityEngine.Random.Range(0, stickClips.Length)];
                audioSource.PlayOneShot(clip);
            }
            else if (hasLaunched && playSound && clips.Length > 0)
            {
                audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
            }

            if(stopwatch2.IsRunning)
            {
                stopwatch2.Stop();
                stopwatch2.Reset();
                stopwatch2.Start();
            }
            else
            {
                stopwatch2.Start();
            }
        }
    }

    //override of AdjustVelocity
    public override void AdjustVelocity()
    {
        if (Math.Abs(_rigidbody.velocity.x) + Math.Abs(_rigidbody.velocity.y) != totalVelcity)
        {
            _rigidbody.velocity = Tools.RestrictVector(_rigidbody.velocity, totalVelcity);
        }

    }
}
