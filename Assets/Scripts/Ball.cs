using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject paddle;
    [SerializeField] AudioClip[] ballSounds;
    
    Vector2 paddleToBallVector;
    public bool ballLocked = true;

    AudioSource hitSound;
    AudioSource cloneSound;

    AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        aSource = GetComponent<AudioSource>();
        hitSound = transform.GetChild(0).GetComponent<AudioSource>();
        cloneSound = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballLocked)
        {
            LockBallToPaddle();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ballLocked && collision.gameObject.tag != "Breakable")
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            aSource.PlayOneShot(clip);
        }
    }
}
