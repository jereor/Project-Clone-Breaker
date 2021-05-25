using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Block : MonoBehaviour
{
    // Config parameters
    [SerializeField] AudioClip destroyClip;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;

    // Cached references
    LevelManager levelManager;
    GameStatus gameStatus;

    // State variables
    [SerializeField] int timesHit;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        levelManager = FindObjectOfType<LevelManager>();
        CountToBlocks();
    }

    private void CountToBlocks()
    {
        if (tag == "Breakable") levelManager.BlockAdded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            if (timesHit++ >= maxHits)
                DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(destroyClip, Camera.main.transform.position);
        Destroy(gameObject);
        TriggerSparklesVFX();
        levelManager.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
    }
}
