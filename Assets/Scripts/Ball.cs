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
        if (ballLocked)
            LockBallToPaddle();
    }

    private void InitializeCachedReferences()
    {
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
        if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Unbreakable")
            ScreenShakeController.instance.startShake();

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
