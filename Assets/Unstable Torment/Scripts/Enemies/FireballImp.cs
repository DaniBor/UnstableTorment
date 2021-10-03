using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballImp : MonoBehaviour
{
    private PlayerScript player;
    private Vector2 movement;
    public GameObject fireballPrefab;
    private Manager mng;

    private Rigidbody2D rb;

    public int health = 4;
    public float basespeed = 4f;
    public float speed = 4f;
    private int pointValue = 50;
    private float desiredDistance = 5f;
    private bool isFiring = false;
    private int damage = 2;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mng = FindObjectOfType<Manager>();
        player = FindObjectOfType<PlayerScript>();
    }

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
    private void Update()
    {
        if (!isFiring){
            Vector3 direction = player.transform.position - transform.position;

            direction.Normalize();
            movement = direction;
        }
        else
        {
            movement = Vector2.zero;
        }
        
    }

    private void FixedUpdate()
    {
        if (mng.curState == Manager.GameState.PLAYERNORMAL) speed = basespeed;
        else if (mng.curState == Manager.GameState.PLAYERENRAGED) speed = basespeed + 2f;
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (desiredDistance > distance)
        {
            if(!isFiring) StartCoroutine(FireBall());
        }
    }

    private IEnumerator FireBall()
    {
        isFiring = true;
        yield return new WaitForSeconds(.6f);

        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        isFiring = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerScript>().DamageMe(damage);
        }
    }
}
