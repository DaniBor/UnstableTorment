using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemybasicFunctions : MonoBehaviour //Unused
{
    public int health;
    public float speed;
    private int pointValue;

    public void DamageMe()
    {
        health--;

        if (health == 0)
        {
            KillMe();
        }
    }

    private void KillMe()
    {
        StatsTracker.AddToScore(pointValue);
        FindObjectOfType<Manager>().ChangeScoreBoard();
        Destroy(gameObject);
    }
}
