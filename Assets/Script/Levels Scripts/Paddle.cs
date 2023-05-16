using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
    [SerializeField] int screenWidthInUnits;
    [SerializeField] float paddleYPosition;
    [SerializeField] float halfPaddleWidth;

    // Update is called once per frame.
    private void Update()
    {
        UpdatePaddlePosition(GetMouseXPosition());
    }

    //takes mouse position and updates the paddle position.
    public void UpdatePaddlePosition(Vector2 pos)
    {
        /*
        if (pos.x < halfPaddleWidth)
            pos.x = halfPaddleWidth;
        else if (pos.x > screenWidthInUnits - halfPaddleWidth)
            pos.x = screenWidthInUnits - halfPaddleWidth;
        */

        transform.position = new Vector2(Mathf.Clamp(pos.x, this.halfPaddleWidth, this.screenWidthInUnits - this.halfPaddleWidth), pos.y);
    }

    //takes a y value and return a new Vector2 with mouse x position and y value given in.
    public Vector2 GetMouseXPosition()
    {
        float mousePosition = Input.mousePosition.x / Screen.width * this.screenWidthInUnits;
        return new Vector2(mousePosition, this.paddleYPosition);
    }

}
