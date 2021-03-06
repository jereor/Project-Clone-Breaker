using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 0.9f;
    [SerializeField] float maxX = 5.9f;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 15f;

    [SerializeField] GameObject ball;

    public bool isNotTouching;
    Touch touch;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0); // Update always gets the first touch
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position); // Convert the touchPos into world units 
            touchPos.z = 0f; // Reset the Z

            // Store the current paddle position
            // and make sure the paddle is clamped to the level borders
            Vector2 paddlePos = new Vector2(Mathf.Clamp(touchPos.x, minX, maxX), transform.position.y);

            // Update paddle position with the new Vector2
            transform.position = paddlePos;

            // Check if ball is locked to the paddle and wait for touch release to launch the ball
            if (ball.GetComponent<Ball>().ballLocked == true && Input.touches[0].phase == TouchPhase.Ended)
                LaunchBall();
        }
    }

    private void LaunchBall()
    {
        ball.GetComponent<Ball>().ballLocked = false;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
    }
}
