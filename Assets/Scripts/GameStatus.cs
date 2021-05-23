using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int currentScore = 0;
    [SerializeField] int pointsPerBlockDestroyed;
    [SerializeField] TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        scoreDisplay.text = GetScore().ToString();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreDisplay.text = GetScore().ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
