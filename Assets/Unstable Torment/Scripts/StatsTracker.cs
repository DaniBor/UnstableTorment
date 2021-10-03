using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatsTracker
{
    public static int curScore;
    public static int highScore;
    public static int highestFloor;
    public static int curFloor;
    public static int currentTier = 0;
    public static int scoreForNextTier = 500;

    public static int clawdamage = 1;
    public static int clawtier = 0;

    public static void AddToScore(int value)
    {
        curScore += value;
        if (curScore > highScore) highScore = curScore;
    }

    public static void reset()
    {
        if(curScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", curScore);
            Debug.Log(PlayerPrefs.GetInt("highscore"));
        }

        curScore = 0;
        scoreForNextTier = 500;
        currentTier = 0;
        clawdamage = 1;
        clawtier = 0;
    }
}
