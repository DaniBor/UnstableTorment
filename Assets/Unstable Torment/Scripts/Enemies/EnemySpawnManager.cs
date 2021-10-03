using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemySpawner[] mySpawners = new EnemySpawner[4];

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ActivateMySpawners()
    {
        foreach(EnemySpawner spawner in mySpawners)
        {
            spawner.gameObject.SetActive(true);
        }
    }

    public void SetMySpawns(GameObject[] prefabs)
    {
        foreach(EnemySpawner spawner in mySpawners)
        {
            spawner.enemyPrefabs = prefabs;
        }
    }

    public void SetSingleSpawner(int index, GameObject[] prefabs)
    {
        mySpawners[index].enemyPrefabs = prefabs;
    }

    public void SetTimers(int min, int max)
    {
        foreach(EnemySpawner spawners in mySpawners)
        {
            spawners.minTimer = min;
            spawners.maxTimer = max;
        }
    }

    public void SetTimer(int index, int min, int max)
    {
        mySpawners[index].minTimer = min;
        mySpawners[index].maxTimer = max;
    }
}
