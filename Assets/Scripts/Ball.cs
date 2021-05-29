using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] GameObject paddle;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.1f;
    
    // State variables
    Vector2 paddleToBallVector;
    public bool ballLocked = true;

    // Cached references
    ScreenShakeController shakeController;

    // Cached component references
    AudioSource audioSource;
    AudioSource hitSound;
    AudioSource cloneSound;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        InitializeCachedReferences();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ball velocity: " + rb.velocity.x + ", " + rb.velocity.y);

        if (ballLocked)
            LockBallToPaddle();
    }

    private void InitializeCachedReferences()
    {
        shakeController = FindObjectOfType<ScreenShakeController>();
        audioSource = GetComponent<AudioSource>();
        hitSound = transform.GetChild(0).GetComponent<AudioSource>();
        cloneSound = transform.GetChild(1).GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shakeController.startShake();

        if (!ballLocked && collision.gameObject.tag != "Breakable")
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);

            if (rb.velocity.y <= 1 && rb.velocity.y >= -1)
            {
                float y = Random.Range(0f, randomFactor);
                Vector2 velocityTweak = new Vector2(0, -y);
                rb.velocity += velocityTweak;
            }
        }
    }
}
