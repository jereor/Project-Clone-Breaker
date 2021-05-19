using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1.5f;
    [SerializeField] float maxX = 20f;
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

            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); // Store the current paddle position
            paddlePos.x = Mathf.Clamp(touchPos.x, minX, maxX); // Make sure the paddle is clamped to the level borders
            transform.position = paddlePos; // Update paddle position with the new Vector2

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
