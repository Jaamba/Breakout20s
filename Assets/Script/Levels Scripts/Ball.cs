using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;

public class Ball : MonoBehaviour
{
    //Config params.
    [SerializeField] protected Paddle paddle;
    [SerializeField] protected Vector2 speedOnLaunch;
    [SerializeField] protected bool playSound;
    [SerializeField] protected AudioClip[] clips;
    [SerializeField] protected float rallentySpeed = 1;
    [SerializeField] protected LevelCanvas canvas;
    [SerializeField] protected float randomDirectionLimit;

    //params.
    protected bool hasLaunched = false;
    protected Vector2 ballAndPaddleOffset;
    protected AudioSource audioSource;
    protected Vector2 direction;
    private Vector2 prevPosition;
    protected bool hasStartedCalculatingDirection = false;
    public bool CanPlayRallenty { get; set; } = false;
    protected Level currentLevel;
    protected float totalVelcity;
    private Rigidbody2D rigidbody;

    //Start is called before the first frame update
    virtual protected void Start()
    {
        this.ballAndPaddleOffset = CalculateBallAndPaddleOffset();
        this.audioSource = GetComponent<AudioSource>();
        this.prevPosition = new Vector2(0, 0);
        this.currentLevel = FindObjectOfType<Level>();
        this.hasLaunched = false;
        this.totalVelcity = Math.Abs(speedOnLaunch.x) + Math.Abs(speedOnLaunch.y);
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame.
    virtual protected void Update()
    {
        if (!hasLaunched)
        {
            Tools.CheckAndDo(KeyCode.Mouse1, Launch);
            transform.position = CalculateBallPositionBeforeLaunch();
        }
        else
        {
            AdjustVelocity();
        }

        CalculateDirection();
        AdjustRotation();
    }

    //resets the ball
    public virtual void Reset()
    {
        hasLaunched = false;
        hasStartedCalculatingDirection = false;
        CanPlayRallenty = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.ballAndPaddleOffset = CalculateBallAndPaddleOffset();
    }

    //adjusts the velocity of the ball
    //if you use a derived class you should always override this method
    public virtual void AdjustVelocity()
    {
        if(Math.Abs(rigidbody.velocity.x) + Math.Abs(rigidbody.velocity.y) != totalVelcity)
        {
            rigidbody.velocity = Tools.RestrictVector(rigidbody.velocity, totalVelcity);
        }
    }

    //launches the ball
    virtual public void Launch()
    {
        GetComponent<Rigidbody2D>().velocity = this.speedOnLaunch;
        hasLaunched = true;
    }

    //calculates the distance between ball and paddle.
    public Vector2 CalculateBallAndPaddleOffset()
    {
        return transform.position - this.paddle.transform.position;
    }

    //calculates ball position.
    public Vector2 CalculateBallPositionBeforeLaunch()
    {
        return new Vector2(this.paddle.transform.position.x + this.ballAndPaddleOffset.x,
            this.paddle.transform.position.y + this.ballAndPaddleOffset.y);
    }

    //plays the sound when the ball collides with something.
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasLaunched && playSound && clips.Length > 0)
        {
            audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
        }

        AddRandomDirection();
    }

    //makes sure that the ball is going to beat the last block and actually plays the rallenty.
    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        if(CanPlayRallenty && FindObjectOfType<Block>() != null)
        {
            Block lastBlock = FindObjectOfType<Block>();

            if(collider.name == lastBlock.name && lastBlock.BlockState == BlockState.reallyBroken)
            {
                Time.timeScale = rallentySpeed;
            }
        }
    }
    virtual protected void OnTriggerExit2D(Collider2D collider)
    {
        this.currentLevel = FindObjectOfType<Level>();

        if (Time.timeScale != 1 && !currentLevel.HasWon)
        {
            Time.timeScale = 1;
        }
    }

    //adjusts the trigger for rallenty's direction
    private void AdjustRotation()
    {
        GetComponent<BoxCollider2D>().transform.rotation = Quaternion.Euler(0, 0, Tools.Vector2ToAngle(direction));
    }

    //adds a random direction
    public void AddRandomDirection()
    {
        if (Tools.RandomBool())
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2(randomDirectionLimit, randomDirectionLimit * -1);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2( randomDirectionLimit * -1, randomDirectionLimit);
        }
    }

    /*
    //methods for calculating the direction:
    private IEnumerator ICalculateDirection()
    {
        hasStartedCalculatingDirection = true;

        if (prevPosition.x == 0 && prevPosition.y == 0)
        {
            prevPosition = transform.position;
        }
        else
        {
            direction = new Vector2(transform.position.x * -1, transform.position.y) - new Vector2(prevPosition.x * -1, prevPosition.y);
            prevPosition = new Vector2(0, 0);
        }

        yield return new WaitForSeconds(directionCountDelay);
        hasStartedCalculatingDirection = false;
    }
    */
    protected void CalculateDirection()
    {
        /*
        if(!hasStartedCalculatingDirection)
        {
            StartCoroutine(ICalculateDirection());
        }
        */

        direction = GetComponent<Rigidbody2D>().velocity;
    }

    //destoys the ball
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
