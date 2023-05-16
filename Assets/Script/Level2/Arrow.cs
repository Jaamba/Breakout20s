using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //params
    [SerializeField] ChuchuJelly ball;
    [SerializeField] float rotationVelocity;

    //fields
    float rotation;
    bool rotatesRight;
    public int specialCounter { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        rotation = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, 1);

        if(ball.StopsMoving)
        {
            if (specialCounter > 50)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<PolygonCollider2D>().enabled = true;
                Move();
            }
            else
            {
                specialCounter++;
            }

        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    //moves the arrow
    private void Move()
    {
        if(rotatesRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            rotation += rotationVelocity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            rotation -= rotationVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("stickable"))
            rotatesRight = !rotatesRight;
    }

    //get the ball velocity
    public Vector2 ballVelocity(int nMultiplier)
    {
        if(transform.rotation.eulerAngles.z > 270 && transform.rotation.eulerAngles.z < 360)
        {
            Vector2 toReturn = Tools.RotationToVector2(360 - transform.rotation.eulerAngles.z, -1 * nMultiplier, nMultiplier);
            return Tools.RestrictVector(toReturn, ball.TotalVelocity);
        }
        else if(transform.rotation.eulerAngles.z > 180 && transform.rotation.eulerAngles.z < 270)
        {
            Vector2 toReturn = Tools.RotationToVector2(180 - transform.rotation.eulerAngles.z, nMultiplier, nMultiplier);
            return Tools.RestrictVector(toReturn, ball.TotalVelocity);
        }
        else if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 180)
        {
            Vector2 toReturn = Tools.RotationToVector2(180 - transform.rotation.eulerAngles.z, nMultiplier, nMultiplier * -1);
            return Tools.RestrictVector(toReturn, ball.TotalVelocity);
        }
        else if (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 90)
        {
            Vector2 toReturn = Tools.RotationToVector2(transform.rotation.eulerAngles.z, nMultiplier * -1, nMultiplier * -1);
            return Tools.RestrictVector(toReturn, ball.TotalVelocity);
        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            return new Vector2(0, ball.TotalVelocity * -1);
        }
        else if(transform.rotation.eulerAngles.z == 180)
        {
            return new Vector2(ball.TotalVelocity, 0);
        }
        else if (transform.rotation.eulerAngles.z == 270)
        {
            return new Vector2(0, ball.TotalVelocity);
        }
        else if (transform.rotation.eulerAngles.z == 360)
        {
            return new Vector2(ball.TotalVelocity * -1, 0);
        }
        else
        {
            return new Vector2();
        }
    }
}
