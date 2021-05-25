using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Block : MonoBehaviour
{
    // Config parameters
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip destroyClip;
    [SerializeField] AudioClip clinkClip;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // Cached references
    LevelManager levelManager;
    GameStatus gameStatus;
    public SpriteRenderer spriteRenderer;

    // State variables
    [SerializeField] int timesHit;

    private void Start()
    {
        InitializeCachedReferences();
        CountToBlocks();
    }

    private void InitializeCachedReferences()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void CountToBlocks()
    {
        if (tag == "Breakable") levelManager.BlockAdded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable") HandleHit();
        else AudioSource.PlayClipAtPoint(clinkClip, Camera.main.transform.position);
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
            DamageBlock();
    }

    private void DamageBlock()
    {
        AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position);
        int spriteIndex = timesHit - 1;
        spriteRenderer.sprite = hitSprites[spriteIndex];
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
