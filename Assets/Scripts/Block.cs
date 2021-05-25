using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Block : MonoBehaviour
{
    [SerializeField] AudioClip destroyClip;
    [SerializeField] GameObject blockSparklesVFX;

    LevelManager levelManager;
    GameStatus gameStatus;

    private void Start()
    {
        CountToBlocks();
    }

    private void CountToBlocks()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        levelManager = FindObjectOfType<LevelManager>();
        if (tag == "Breakable") levelManager.BlockAdded();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable") DestroyBlock();
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
