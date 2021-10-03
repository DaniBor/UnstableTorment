using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicImp : MonoBehaviour
{
    private Manager mng;
    private PlayerScript player;
    private Vector2 movement;

    private Rigidbody2D rb;

    public int health = 2;
    public float speed = 4f;
    public float basespeed = 4f;
    private int pointValue = 35;
    private int damage = 3;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerScript>();
        mng = FindObjectOfType<Manager>();

    }

    public void DamageMe()
    {
        health--;

        if(health == 0)
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
        Vector3 direction = player.transform.position - transform.position;

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction)
    {
        if (mng.curState == Manager.GameState.PLAYERNORMAL) speed = basespeed;
        else if (mng.curState == Manager.GameState.PLAYERENRAGED) speed = basespeed + 2f;
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerScript>().DamageMe(damage);
        }
    }
}
