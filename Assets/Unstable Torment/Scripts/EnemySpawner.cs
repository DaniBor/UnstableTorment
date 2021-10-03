using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int timer;
    public bool canSpawn = true;
    public int minTimer = 1;
    public int maxTimer = 5;

    public GameObject[] enemyPrefabs;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int rng = Random.Range(1, 11);
            switch (enemyPrefabs.Length)
            {
                case 1:
                    Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
                    break;
                case 2:
                    if (rng <= 3) Instantiate(enemyPrefabs[1], transform.position, Quaternion.identity);
                    else Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
                    break;
                case 3:
                    if (rng == 1) Instantiate(enemyPrefabs[2], transform.position, Quaternion.identity);
                    else if (rng <= 4) Instantiate(enemyPrefabs[1], transform.position, Quaternion.identity);
                    else Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
                    break;
            }





            timer = Random.Range(minTimer, maxTimer);
            yield return new WaitForSeconds(timer);





            if(canSpawn == false) yield return new WaitUntil(() => canSpawn == true);
        }
    }
}
