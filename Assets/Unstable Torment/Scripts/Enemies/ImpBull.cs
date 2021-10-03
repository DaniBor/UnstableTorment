using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpBull : MonoBehaviour
{
    private Manager mng;
    private PlayerScript player;
    private Vector2 movement;
    private Vector3 direction;

    private Rigidbody2D rb;

    public int health = 6;
    public float speed = 4f;
    public float basespeed = 4f;
    public float chargespeed = 11f;
    private int pointValue = 100;
    private int damage = 7;
    public bool isCharging = false;
    private float chargetime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerScript>();
        mng = FindObjectOfType<Manager>();

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
        if(transform.position.y >= player.transform.position.y)
        {
            direction = player.transform.position - transform.position;
            direction.y += 2f;
        }
        else
        {
            direction = player.transform.position - transform.position;
            direction.y -= 2f;
        }

        direction.Normalize();
        movement = direction;

        if (chargetime < Time.time) isCharging = false;
    }

    private void FixedUpdate()
    {
        if (!isCharging) moveCharacter(movement);
        else Charge();
    }

    private void moveCharacter(Vector2 direction)
    {
        if (mng.curState == Manager.GameState.PLAYERNORMAL) speed = basespeed;
        else if (mng.curState == Manager.GameState.PLAYERENRAGED) speed = basespeed + 2f;
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));

        if(Mathf.Abs(player.transform.position.y - transform.position.y) < 3f && Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f)
        {
            isCharging = true;
            chargetime = Time.time + 1.5f;
        }
    }

    private void Charge()
    {
        Vector2 chargeDir;
        if (mng.curState == Manager.GameState.PLAYERNORMAL) speed = basespeed;
        else if (mng.curState == Manager.GameState.PLAYERENRAGED) speed = basespeed + 2f;

        if (transform.position.y >= player.transform.position.y) chargeDir = new Vector2(0, -1);
        else chargeDir = new Vector2(0, 1);


        rb.MovePosition((Vector2)transform.position + (chargeDir * chargespeed * Time.deltaTime));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerScript>().DamageMe(damage);
        }
    }
}
