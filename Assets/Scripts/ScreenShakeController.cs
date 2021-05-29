using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float shakeTime = 0.1f;
    [SerializeField] float initialShakePower = 0.1f;

    // Cached references
    private Vector3 targetPos;

    // State variables
    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {    
        transform.position = targetPos;
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }

    public void startShake()
    {
        shakeTimeRemaining = shakeTime;
        shakePower = initialShakePower;

        shakeFadeTime = initialShakePower / shakeTime;
    }
}
