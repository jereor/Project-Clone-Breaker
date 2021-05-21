using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField] int currentScore = 0;
    [SerializeField] int pointsPerBlockDestroyed;

    public int GetScore()
    {
        return currentScore;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
    }
}
