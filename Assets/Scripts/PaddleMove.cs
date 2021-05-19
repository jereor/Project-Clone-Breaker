using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMove : MonoBehaviour
{
    [SerializeField] float minX = 1.5f;
    [SerializeField] float maxX = 20f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Update always gets the first touch
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position); // Convert the touchPos into world units 
            touchPos.z = 0f; // Reset the Z

            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y); // Store the current paddle position
            paddlePos.x = Mathf.Clamp(touchPos.x, minX, maxX); // Make sure the paddle is clamped to the level borders
            transform.position = paddlePos; // Update paddle position with the new Vector2
        }
    }
}
