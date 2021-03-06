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
    [SerializeField] Sprite[] hitSprites;

    // Cached references
    LevelManager levelManager;
    GameStatus gameStatus;
    SpriteRenderer spriteRenderer;

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
        if (CompareTag("Breakable")) levelManager.BlockAdded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable")) HandleHit();
        else AudioSource.PlayClipAtPoint(clinkClip, Camera.main.transform.position);
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
            DamageBlock();
    }

    private void DamageBlock()
    {
        ScreenShakeController.instance.StartShake(.1f, .1f);

        AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position);
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
            spriteRenderer.sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Block sprite is missing from array in object " + gameObject.name);
    }

    private void DestroyBlock()
    {
        ScreenShakeController.instance.StartShake(.3f, .15f);

        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(destroyClip, Camera.main.transform.position);
        Destroy(gameObject);
        TriggerSparklesVFX();
        levelManager.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        Instantiate(blockSparklesVFX, transform.position, transform.rotation);
    }
}
