using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private PlayerScript player;
    private Rigidbody2D rb;
    private Manager mng;
    private float timer = 10;

    private Vector2 movement;
    public float basespeed = 4f;
    public float speed = 4f;
    private int damage = 5;

    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
        mng = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();
        movement = direction;
        timer = timer + Time.time;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
        if (timer < Time.time) Destroy(gameObject);
    }

    private void moveCharacter(Vector2 direction)
    {
        if (mng.curState == Manager.GameState.PLAYERNORMAL) speed = basespeed;
        else if (mng.curState == Manager.GameState.PLAYERENRAGED) speed = basespeed + 2f;
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerScript>().DamageMe(damage);
        }
    }
}
